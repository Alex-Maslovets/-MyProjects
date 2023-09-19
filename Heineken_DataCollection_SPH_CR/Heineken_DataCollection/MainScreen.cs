﻿using Npgsql;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

//using Microsoft.Extensions.Logging;
//using Telegram.Bot.Exceptions;
//using Telegram.Bot.Extensions.Polling;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.ReplyMarkups;
//using System.Threading;

namespace Heineken_DataCollection
{
    public partial class MainScreen : Form
    {

        const int numberOfMessage = 50;
        bool[] previousMessageState = new bool[numberOfMessage];
        string[] messageText = new string[numberOfMessage];
        bool firstScan = false;
        bool firstStart = false;

        public int seconds_last = new int();
        public int minutes_last = new int();
        public int hours_last = new int();
        public int days_last = new int();

        public int seconds_last_mb = new int();
        public int minutes_last_mb = new int();
        public int hours_last_mb = new int();
        public int days_last_mb = new int();

        string alarmMessagesArchivePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\messageArchive.txt";

        public MainScreen()
        {
            InitializeComponent();
            backgroundWorkerRead.WorkerReportsProgress = true;
            backgroundWorkerRead.WorkerSupportsCancellation = true;
            backgroundWorkerRead.DoWork += new DoWorkEventHandler(BackgroundWorkerRead_DoWork);
            backgroundWorkerRead.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerRead_RunWorkerCompleted);

            // Alarm - 🟥; Warning - 🟧; Info - 🟦
            messageText[0] = "🟥 Alarm Reserve 0";
            messageText[1] = "🟥 Alarm Reserve 1";
            messageText[2] = "🟥 Alarm Reserve 2";
            messageText[3] = "🟥 Alarm Reserve 3";
            messageText[4] = "🟥 Alarm Reserve 4";
            messageText[5] = "🟥 Показания СО2 прибора ZIROX \\>\\= 10\\ ppm";
            messageText[6] = "🟥 Нажата аварийная кнопка в аммиачном/СО2 отделении";
            messageText[7] = "🟥 Уровень в отделители NH3 \\>\\= 40\\%";
            messageText[8] = "🟥 Уровень в отделители NH3 \\<\\= 10\\%";
            messageText[9] = "🟥 Уровень наполнения газгольдера \\>\\= 95\\%";
            messageText[10] = "🟥 Давление в танке хранения СО2 \\>\\= 16 Бар";
            messageText[11] = "🟥 Давление на подаче NH3 \\>\\= 6 Бар";
            messageText[12] = "🟥 Давление конденсации \\>\\= 13\\,5 Бар";
            messageText[13] = "🟥 Просадка давления пара";
            messageText[14] = "🟥 Неисправность аммиачного компрессора №1";
            messageText[15] = "🟥 Неисправность аммиачного компрессора №2";
            messageText[16] = "🟥 Неисправность аммиачного компрессора №3";
            messageText[17] = "🟥 Отключение электроэнергии";
            messageText[18] = "🟥 Уровень воды в баке оборотного водоснабжения \\>\\= 95\\%";
            messageText[19] = "🟥 Уровень воды в баке оборотного водоснабжения \\<\\= 25\\%";
            messageText[20] = "🟥 Температура ледяной воды \\>\\= 6\\,5 град\\. С";
            messageText[21] = "🟥 Температура ледяной воды \\<\\= 0\\,0 град\\. С";
        }

        // Read S7
        private void Button_Read_s7_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorkerRead.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    backgroundWorkerRead.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_notRead_s7_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorkerRead.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerRead.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void ReadwriteS7()
        {

            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection(
                "Host=localhost;" +
                "Username=postgres;" +
                "Password=123456;" +
                "Database=postgres;" +
                "Timeout = 300;" +
                "CommandTimeout = 300");

            try
            {
                PGCon.Open();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);

                foreach (var frame in trace.GetFrames())
                {
                    var sb = new StringBuilder();

                    sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                    sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                    sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                    sb.Append($"Метод: {frame.GetMethod()}");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                            sw.Write("Siemens; " + DateTime.Now + "; " + sb + ";\n");
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
                PGCon.Close();
            }

            if (PGCon.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    if (!firstStart)
                    {
                        var cmd_select_time = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "SELECT date_time FROM _minutes_table ORDER BY date_time DESC LIMIT 1"
                        };
                        NpgsqlDataReader reader = cmd_select_time.ExecuteReader();
                        reader.Read();

                        seconds_last = reader.GetDateTime(0).Second;
                        minutes_last = reader.GetDateTime(0).Minute;
                        hours_last = reader.GetDateTime(0).Hour;
                        days_last = reader.GetDateTime(0).Day;

                    }
                    firstStart = true;

                    DateTime s1 = DateTime.Now;

                    List<string> myList = new List<string>();

                    byte[] db1Buffer = new byte[128];

                    // Установка соединения с PLC в щитовой ТП-3679
                    S7Client plcClient = new S7Client();
                    int result = plcClient.ConnectTo("10.129.31.147", 0, 2);

                    result = plcClient.DBRead(20, 0, 128, db1Buffer);
                    if (result != 0)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("S7; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= 31; i++)
                        {
                            double db1ddd4 = S7.GetRealAt(db1Buffer, 4 * i);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                    }

                    plcClient.Disconnect();

                    // Соединение и считывание данных с контроллера в энергоблоке
                    result = plcClient.ConnectTo("10.129.31.135", 0, 3);
                    byte[] db2Buffer = new byte[160];

                    result = plcClient.DBRead(2000, 1838, 160, db2Buffer);
                    if (result != 0)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("S7; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        for (int i = 32; i <= 71; i++)
                        {
                            double db2ddd4 = S7.GetRealAt(db2Buffer, 4 * (i - 32));
                            myList.Add("(" + i + "," + db2ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                    }

                    ////////// Работа с сообщениями //////////

                    db2Buffer = new byte[3];

                    result = plcClient.DBRead(2000, 8, 3, db2Buffer);
                    if (result != 0)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("Messages; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        bool[] currentMessageState = new bool[numberOfMessage];
                        bool[] createMessage = new bool[numberOfMessage];
                        string[] messageType = new string[numberOfMessage];

                        for (int i = 0; i < db2Buffer.Length; i++)
                        {
                            for (int j = 0; j <= 7; j++)
                            {
                                bool bit = S7.GetBitAt(db2Buffer, i, j);
                                currentMessageState[i * 8 + j] = bit;
                            }
                        }

                        for (int i = 0; i < currentMessageState.Length; i++)
                        {
                            if (previousMessageState[i] != currentMessageState[i] && currentMessageState[i] == true)
                            {
                                previousMessageState[i] = currentMessageState[i];
                                createMessage[i] = true;
                                messageType[i] = "⬆️";
                            }
                            else if (previousMessageState[i] != currentMessageState[i] && currentMessageState[i] == false)
                            {
                                previousMessageState[i] = currentMessageState[i];
                                createMessage[i] = true;
                                messageType[i] = "⬇️";
                            }
                        }

                        if (firstScan)
                        {
                            for (int i = 0; i < createMessage.Length; i++)
                            {
                                if (createMessage[i] == true)
                                {
                                    var webProxy = new WebProxy(Host: "10.129.24.100", Port: 8080)
                                    {
                                        // Credentials if needed:
                                        // Credentials = new NetworkCredential("USERNAME", "PASSWORD")
                                    };
                                    var httpClient = new HttpClient(
                                        new HttpClientHandler { Proxy = webProxy, UseProxy = true }
                                    );

                                    var botClient = new TelegramBotClient("5211488879:AAEy5YGotJ1bK-vyegu1DaUVI-XDh98vCT4", httpClient);

                                    //var me = await botClient.GetMeAsync();

                                    Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                                        chatId: "-1001749496684",//chatId,
                                        text: messageType[i] + messageText[i],
                                    parseMode: ParseMode.MarkdownV2,
                                    disableNotification: true);
                                }
                            }
                            createMessage = null;
                        }
                        firstScan = true;
                    }

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    plcClient.Disconnect();

                    var sqlValues = String.Join(", ", myList.ToArray());

                    int seconds_now = DateTime.Now.Second;
                    int minutes_now = DateTime.Now.Minute;
                    int hours_now = DateTime.Now.Hour;
                    int days_now = DateTime.Now.Day;

                    if (seconds_now != seconds_last)
                    {
                        seconds_last = seconds_now;
                        // Запись данных в PostgreSQL 1 раз в 1 секунду
                        var cmd_sec_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _seconds_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_sec_insert.ExecuteNonQuery();
                    }
                    if (minutes_now != minutes_last)
                    {
                        minutes_last = minutes_now;
                        // Запись данных в PostgreSQL 1 раз в 1 минуту
                        var cmd_min_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _minutes_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_min_insert.ExecuteNonQuery();
                    }
                    if (hours_now != hours_last)
                    {
                        hours_last = hours_now;
                        // Запись данных в PostgreSQL 1 раз в 1 час
                        var cmd_hour_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _hours_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_hour_insert.ExecuteNonQuery();
                    }
                    if (days_now != days_last)
                    {
                        days_last = days_now;
                        // Запись данных в PostgreSQL 1 раз в 1 день
                        var cmd_day_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _days_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_day_insert.ExecuteNonQuery();
                    }

                    progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Marquee));

                    timeLabel_s7.Invoke(new Action(() => timeLabel_s7.Text = "Время последнего цикла: " + DateTime.Now.Subtract(s1)));

                }
                catch (Exception ex)
                {
                    var trace = new StackTrace(ex, true);

                    foreach (var frame in trace.GetFrames())
                    {
                        var sb = new StringBuilder();

                        sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                        sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                        sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                        sb.Append($"Метод: {frame.GetMethod()}");

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("Siemens; " + DateTime.Now + "; " + sb + ";\n");
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show(exe.Message);
                        }
                    }

                }
            }
            // Закрытие соединения с PostgreSQL
            PGCon.Close();
        }
        private void BackgroundWorkerRead_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    ReadwriteS7();
                }
            }
        }
        private void BackgroundWorkerRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Value = 0));
            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Blocks));
        }
    }
}