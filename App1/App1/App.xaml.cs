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

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

            try
            {
                Console.WriteLine("OnStart wurde aufgerufen."); // Hinzugefügt zur Überprüfung
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydb.db3"); 
                Database = new SQLiteConnection(dbPath);
                Database.CreateTable<App1.Models.User>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Erstellen der Datenbank: {ex}");
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
