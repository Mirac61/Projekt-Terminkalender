using App1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using App1.Services;
using System;

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

            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

            // Überprüfen, ob die Datenbankdatei existiert
            if (!File.Exists(dbPath))
            {
                // Wenn nicht, erstellen Sie die Datenbankdatei
                File.Create(dbPath).Dispose();
            }

            Database = new SQLiteConnection(dbPath);
            Database.CreateTable<App1.Models.User.Benutzer>();

            // Setze die MainPage auf die LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            try
            {
                Console.WriteLine("OnStart wurde aufgerufen.");

                Database = new SQLiteConnection(dbPath);

                if (!File.Exists(dbPath))
                {
                    Database.CreateTable<App1.Models.User.Benutzer>();
                    Console.WriteLine("Datenbank und Tabelle erstellt.");
                }
                else
                {
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
            Database?.Close();
        }

        protected override void OnResume()
        {
        }
    }
}
