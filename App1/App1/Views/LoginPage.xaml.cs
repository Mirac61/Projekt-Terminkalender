using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using App1.Models;


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

            private void LoginButton_Clicked(object sender, EventArgs e)
            {

                string username = UsernameEntry.Text;
                string password = PasswordEntry.Text;

                
                    if (IsValidUser(username, password))
                    {
                        // Anmeldung erfolgreich
                        Navigation.PushAsync(new AboutPage());
                    }
                    else
                    {

                        ErrorLabel.IsVisible = true;
                        ErrorLabel.Text = "Ungültige Anmeldeinformationen.";
                    }
                
               
            }

            private bool IsValidUser(string username, string password)
            {

                var existingUser = App.Database.Table<User.UserD>().FirstOrDefault(u => u.Username == username);

            
                if (existingUser != null && existingUser.Password == password)
                {
                    return true; 
                }

                return false; 

            }

            private void RegisterButton_Clicked(object sender, EventArgs e)
            {
                string username = UsernameEntry.Text;
                string password = PasswordEntry.Text;

                if (IsUsernameAvailable(username))
                {
                    // Benutzer registrieren
                    CreateUser(username, password);

                    // Erfolgreiche Registrierung - Navigieren Sie zur Anmeldeseite
                    Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    // Benutzername bereits vergeben - Zeigen Sie eine Fehlermeldung an
                    ErrorLabel.IsVisible = true;
                    ErrorLabel.Text = "Benutzername bereits vergeben.";
                }
            }

            private bool IsUsernameAvailable(string username)
            {
                var existingUser = App.Database.Table<User.UserD>().FirstOrDefault(u => u.Username == username);


                // Wenn ein Benutzer mit diesem Benutzernamen gefunden wurde, geben Sie true zurück, andernfalls false
                return existingUser != null;
            }

            private void CreateUser(string username, string password)
            {
                try
                {
                    User.UserD newUser = new User.UserD
                    {
                        Username = username, 
                        Password = password  
                    };

                
                    int rowsAffected = App.Database.Insert(newUser);

                    if (rowsAffected > 0)
                    {
                        // Erfolgreich eingefügt
                        // Fügen Sie hier weitere Aktionen hinzu, falls erforderlich
                    }
                    else
                    {
                        // Einfügeoperation ist fehlgeschlagen
                        // Hier können Sie Fehlerbehandlungslogik hinzufügen, z. B. eine Fehlermeldung anzeigen
                        DisplayAlert("Fehler", "Die Registrierung ist fehlgeschlagen.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Behandeln Sie Ausnahmen, die während der Einfügeoperation auftreten könnten
                    Console.WriteLine($"Fehler beim Erstellen des Benutzers: {ex}");
                    DisplayAlert("Fehler", $"Die Registrierung ist fehlgeschlagen: {ex.Message}", "OK"); // Hier können Sie Fehlerbehandlungslogik hinzufügen
                }
            }
        }


    }