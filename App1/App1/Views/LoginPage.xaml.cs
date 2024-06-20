using App1.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using App1.Models;
using System.Linq;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RegisterLabel.IsVisible = true;
        }

        // Überprüft, ob der Benutzer gültig ist
        private bool GueltigerBenutzer(string benutzernameOderEmail, string passwort)
        {
            // Abrufen aller Benutzer aus der Datenbank in eine Liste
            var benutzerListe = App.Database.Table<User.Benutzer>().ToList();
            User.Benutzer bestehenderBenutzer = null;

            // Durchlaufen der Liste der Benutzer
            for (int i = 0; i < benutzerListe.Count; i++)
            {
                // Überprüfen, ob der Benutzername oder die E-Mail-Adresse übereinstimmen
                if (benutzerListe[i].Benutzername == benutzernameOderEmail || benutzerListe[i].Email == benutzernameOderEmail)
                {
                    // Wenn eine Übereinstimmung gefunden wurde, speichern wir den Benutzer und brechen die Schleife ab
                    bestehenderBenutzer = benutzerListe[i];
                    break;
                }
            }

            // Überprüfen, ob der gefundene Benutzer das richtige Passwort hat
            return bestehenderBenutzer != null && bestehenderBenutzer.Password == passwort;
        }

        // Loginbutton-Klick Ereignis
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string benutzernameOderEmail = Benutzername_Eingabe.Text.Trim();
            string passwort = Passwort_Eingabe.Text.Trim();

            if (string.IsNullOrWhiteSpace(benutzernameOderEmail) || string.IsNullOrWhiteSpace(passwort))
            {
                await DisplayAlert("Fehler", "Benutzername oder E-Mail und Passwort dürfen nicht leer sein.", "OK");
                return;
            }

            if (GueltigerBenutzer(benutzernameOderEmail, passwort))
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Fehler", "Ungültige Anmeldeinformationen.", "OK");
            }
        }

        // Weiterleitung zur Registrierungsseite
        public async void OnSignUpTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        // Weiterleitung zur Passwort-Wiederherstellungsseite
        public async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }
    }
}
