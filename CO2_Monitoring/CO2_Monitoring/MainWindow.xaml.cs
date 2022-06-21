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

        public class Phone
        {
            public string Title { get; set; }
            public string Company { get; set; }
            public string State { get; set; }
            public int Price { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainTabControl.SelectedIndex = 2;

            List<Phone> phonesList = new List<Phone>
            {
            new Phone {Title="iPhone 6S", Company="Apple", Price=54990, State = "Авария" },
            new Phone {Title="Lumia 950", Company="Microsoft", Price=39990, State = "Предупреждение" },
            new Phone {Title="Nexus 5X", Company="Google", Price=29990, State = "Авария" }
            };
            Grid_MainMessages.ItemsSource = phonesList;

            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection("Host=localhost;" +
                "Username=postgres;" +
                "Password=123456789;" +
                "Database=postgres;" +
                "Timeout = 300;" +
                "CommandTimeout = 300");
            if (PGCon.State == System.Data.ConnectionState.Open)
            {

                string sql = "SELECT * FROM messages_CO2";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, PGCon))
                {
                    int val;
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        val = Int32.Parse(reader[0].ToString());
                        //do whatever you like
                    }
                }
            }
            // Закрытие соединения с PostgreSQL
            PGCon.Close();
        }
    }
}
