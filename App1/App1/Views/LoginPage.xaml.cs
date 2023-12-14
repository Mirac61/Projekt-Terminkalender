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
                var image = new Image { Source = "Scheduly.png" };

            }


            public bool IsValidUser(string usernameOrEmail, string password)
            {
                var existingUser = App.Database.Table<User.UserD>().FirstOrDefault(u =>
                    u.Username == usernameOrEmail || u.Email == usernameOrEmail);

                if (existingUser != null && existingUser.Password == password)
                {
                    return true;
                }

                return false;
            }


            public void LoginButton_Clicked(object sender, EventArgs e)
            {
                string usernameOrEmail = Benutzername_Eingabe.Text.Trim();
                string password = Passwort_Eingabe.Text.Trim();

                if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
                {
                    DisplayAlert("Fehler", "Benutzername oder E-Mail und Passwort dürfen nicht leer sein.", "OK");
                    return;
                }

                if (IsValidUser(usernameOrEmail, password))
                {
                    Navigation.PushAsync(new AboutPage());
                }
                else
                {
                    DisplayAlert("Fehler", "Ungültige Anmeldeinformationen.", "OK");
                }
            }

    /*  public void RegisterButton_Clicked(object sender, EventArgs e)
     {
         string username = UsernameEntry.Text.Trim();
         string password = PasswordEntry.Text.Trim();
         string email = UsernameEntry.Text.Trim();

         if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
         {
             DisplayAlert("Fehler", "Benutzername und Passwort dürfen nicht leer sein.", "OK");
             return;
         }

        if (IsUsernameAvailable(username, email))
         {
             CreateUser(username, password, email);
         }
         else
         {
             DisplayAlert("Fehler", "Benutzername bereits vergeben.", "OK");
         }*/



            public bool IsUsernameAvailable(string username, string email)
            {
                var existingUser = App.Database.Table<User.UserD>().FirstOrDefault(u => u.Username == username);

                var existingUser2 = App.Database.Table<User.UserD>().FirstOrDefault(u => u.Email == email);

                // Wenn ein Benutzer mit diesem Benutzernamen gefunden wurde, geben Sie true zurück, andernfalls false
                return existingUser != null;
            }

           /* public void CreateUser(string username, string password, string email)
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
            }*/
            public async void OnSignUpTapped(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new RegisterPage());
            }
        }


    }