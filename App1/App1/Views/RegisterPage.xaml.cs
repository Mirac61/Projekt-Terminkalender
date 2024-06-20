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
            // Benutzername, Passwort und E-Mail aus den Eingabefeldern holen
            string benutzername = Benutzername_Eingabe.Text;
            string passwort = Passwort_Eingabe.Text;
            string email = EMail_Eingabe.Text;

            // Überprüft die Regeln für die Registrierung
            if (RegistrierungsRegeln(benutzername, passwort, email))
            {
                // Versucht, einen neuen Benutzer zu erstellen
                if (ErstelleBenutzer(benutzername, passwort, email))
                {
                    // Zeigt eine Erfolgsmeldung an und navigiert zurück
                    await DisplayAlert("Erfolg", "Die Registrierung war erfolgreich.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    // Zeigt eine Fehlermeldung an, wenn das Erstellen des Benutzers fehlschlägt
                    await DisplayAlert("Fehler", "Die Registrierung ist fehlgeschlagen. Bitte versuchen Sie es erneut.", "OK");
                }
            }
            else
            {
                // Zeigt eine Fehlermeldung an, wenn die Registrierungsdaten ungültig sind
                await DisplayAlert("Fehler", "Ungültige Registrierungsdaten. Bitte überprüfen Sie Ihre Eingabe.", "OK");
            }
        }

        // Überprüft die Registrierungsregeln
        private bool RegistrierungsRegeln(string benutzername, string passwort, string email)
        {
            // Überprüft, ob Benutzername, Passwort und E-Mail nicht leer oder null sind mithilfe ChatGPT
            return !string.IsNullOrEmpty(benutzername) && !string.IsNullOrEmpty(passwort) && !string.IsNullOrEmpty(email);
        }

        // Erstellt einen neuen Benutzer - mithilfe ChatGPT
        private bool ErstelleBenutzer(string benutzername, string passwort, string email)
        {
            try
            {
                // Erstellt ein neues Benutzerobjekt mit den übergebenen Daten
                User.Benutzer neuerBenutzer = new User.Benutzer
                {
                    Benutzername = benutzername,
                    Password = passwort,
                    Email = email
                };

                // Fügt den neuen Benutzer in die Datenbank ein
                int Zeilen = App.Database.Insert(neuerBenutzer);

                // Überprüft, ob das Einfügen erfolgreich war
                if (Zeilen <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // Bei Fehlern wird eine Fehlermeldung ausgegeben und false zurückgegeben
                Console.WriteLine($"Fehler beim Erstellen des Benutzers: {ex}");
                return false;
            }
        }
    }
}
