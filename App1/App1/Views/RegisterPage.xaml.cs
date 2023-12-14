using App1.Models;
using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Views;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = Benutzername_Eingabe.Text;
            string password = Passwort_Eingabe.Text;
            string email = EMail_Eingabe.Text;

            if (Registrierungsregeln(username, password, email))
            {
                if (UserErstellen(username, password, email))
                {
                    await DisplayAlert("Erfolg", "Die Registrierung war erfolgreich.", "OK");
                    await Navigation.PopAsync(); // Zurück zur LoginPage
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


        private bool Registrierungsregeln(string username, string password, string email)
        {
            return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(email);
        }

        private bool UserErstellen(string username, string password, string email)
        {
            try
            {
                User.UserD newUser = new User.UserD
                {
                    Username = username,
                    Password = password,
                    Email = email
                };

                int rowsAffected = App.Database.Insert(newUser);

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    DisplayAlert("Fehler", "Nix gut.", "OK");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Erstellen des Benutzers: {ex}");
                DisplayAlert("Fehler", $"Die Registrierung ist fehlgeschlagen: {ex.Message}", "OK");
                return false;
            }
        }



    }
}
