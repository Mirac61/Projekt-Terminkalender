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
            string username = YourUsernameEntry.Text;
            string password = YourPasswordEntry.Text;
            string email = YourEmailEntry.Text;

            if (IsRegistrationValid(username, password, email))
            {
                if (CreateUser(username, password, email))
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


        private bool IsRegistrationValid(string username, string password, string email)
        {

            if (username != null || password != null || email != null)
            {
                return true;
            }
            else
            {
                return false;
            }

            // Hier können Sie weitere benutzerdefinierte Validierungen hinzufügen, falls erforderlich

           
        }

        private bool CreateUser(string username, string password , string email)
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
                return false;
            }
        }


    }
}
