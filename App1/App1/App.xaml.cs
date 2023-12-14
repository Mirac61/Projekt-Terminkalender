using App1.Services;
using App1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using Xamarin.Forms.Shapes;
 



namespace App1
{
    public partial class App : Application
    {
        public static SQLiteConnection Database { get; set; }
        private static string dbPath; // Verschieben Sie die Deklaration hierhin

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

            // Überprüfen, ob die Datenbankdatei existiert
            if (!File.Exists(dbPath))
            {
                // Wenn nicht, erstellen Sie die Datenbankdatei
                File.Create(dbPath).Dispose();
            }

            Database = new SQLiteConnection(dbPath);
            Database.CreateTable<App1.Models.User.UserD>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

            try
            {
                Console.WriteLine("OnStart wurde aufgerufen.");

                Database = new SQLiteConnection(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3"));

                if (!File.Exists(dbPath))
                {
                    Database = new SQLiteConnection(dbPath);
                    Database.CreateTable<App1.Models.User.UserD>();
                    Console.WriteLine("Datenbank und Tabelle erstellt.");
                }
                else
                {
                    Database = new SQLiteConnection(dbPath);
                    Console.WriteLine("Datenbank bereits vorhanden.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Erstellen/Öffnen der Datenbank: {ex}");
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
