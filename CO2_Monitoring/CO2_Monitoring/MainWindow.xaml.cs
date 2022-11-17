using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using Npgsql.EntityFrameworkCore;
using System.Runtime;

using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace CO2_Monitoring
{
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class messageCO2
        {
            public string? Time_Start { get; set; }
            public string? Time_End { get; set; }
            public string? Message_Type { get; set; }
            public string? Message_Text { get; set; }
            public string? Department { get; set; }
            public string? Number { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ARHF_1066_Grp

            //MainTabControl.SelectedIndex = 2;
            try
            {
                // Установка соединения с PostgreSQL
                NpgsqlConnection PGCon = new NpgsqlConnection("Host=localhost;" +
                            "Port=5433;" +
                            "Username=postgres;" +
                            "Password=123456789;" +
                            "Database=postgres");

                PGCon.Open();

                if (PGCon.State == System.Data.ConnectionState.Open)
                {

                    string sql = "SELECT * FROM \"messages_CO2\"";

                    List<messageCO2> messagesList = new List<messageCO2>();

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, PGCon))
                    {

                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            messagesList.Add(new messageCO2
                            {
                                Time_Start = reader[0].ToString(),
                                Time_End = reader[1].ToString(),
                                Message_Type = reader[5].ToString(),
                                Message_Text = reader[4].ToString(),
                                Department = reader[3].ToString(),
                                Number = reader[2].ToString()
                            }
                            );
                        };
                    };

                    Grid_MainMessages.ItemsSource = messagesList;
                    Grid_MainMessages.ScrollIntoView(messagesList[messagesList.Count-1]);
                }
                // Закрытие соединения с PostgreSQL
                PGCon.Close();
            }
            catch (Exception ex)
            {
            MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
