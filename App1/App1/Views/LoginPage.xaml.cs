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
                this.BindingContext = new LoginViewModel();
            }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Stelle sicher, dass der Registrierungslink beim Laden der Seite angezeigt wird
            RegisterLabel.IsVisible = true;
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

            public async void LoginButton_Clicked(object sender, EventArgs e)
            {

                string usernameOrEmail = Benutzername_Eingabe.Text.Trim();
                string password = Passwort_Eingabe.Text.Trim();

                if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
                {
                    await DisplayAlert("Fehler", "Benutzername oder E-Mail und Passwort dürfen nicht leer sein.", "OK");
                    return;
                }

                if (IsValidUser(usernameOrEmail, password))
                {
                    // Zur MainPage (AppShell) navigieren, die die AboutPage enthält
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Fehler", "Ungültige Anmeldeinformationen.", "OK");
                }
            }

            public bool IsUsernameAvailable(string username, string email)
            {
                var existingUser = App.Database.Table<User.UserD>().FirstOrDefault(u => u.Username == username);

                var existingUser2 = App.Database.Table<User.UserD>().FirstOrDefault(u => u.Email == email);

                // Wenn ein Benutzer mit diesem Benutzernamen gefunden wurde, geben Sie true zurück, andernfalls false
                return existingUser != null;
            }

           
            public async void OnSignUpTapped(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new RegisterPage());
            }

            public async void OnForgotPasswordTapped(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new NewItemPage());
            }

    }


    }