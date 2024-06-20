using App1.Models;
using App1;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        // Klick Ereignis für den Registrierungsbutton - mithilfe ChatGPT
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string benutzername = Benutzername_Eingabe.Text;
            string passwort = Passwort_Eingabe.Text;
            string email = EMail_Eingabe.Text;

            if (RegistrierungsRegeln(benutzername, passwort, email))
            {
                if (ErstelleBenutzer(benutzername, passwort, email))
                {
                    await DisplayAlert("Erfolg", "Die Registrierung war erfolgreich.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Fehler", "Die Registrierung ist fehlgeschlagen. Bitte versuchen Sie es erneut.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fehler", "Ungültige Registrierungsdaten. Bitte überprüfen Sie Ihre Eingabe.", "OK");
            }
        }

        // Überprüft die Registrierungsregeln
        private bool RegistrierungsRegeln(string benutzername, string passwort, string email)
        {
            return !string.IsNullOrEmpty(benutzername) && !string.IsNullOrEmpty(passwort) && !string.IsNullOrEmpty(email);
        }

        // Erstellt einen neuen Benutzer - mithilfe ChatGPT
        private bool ErstelleBenutzer(string benutzername, string passwort, string email)
        {
            try
            {
                User.Benutzer neuerBenutzer = new User.Benutzer
                {
                    Benutzername = benutzername,
                    Password = passwort,
                    Email = email
                };

                int Zeilen = App.Database.Insert(neuerBenutzer);

                if (Zeilen <= 0)
                {
                    return false;
                }

                User.InitialisiereDatenbank(benutzername);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Erstellen des Benutzers: {ex}");
                return false;
            }
        }
    }
}
