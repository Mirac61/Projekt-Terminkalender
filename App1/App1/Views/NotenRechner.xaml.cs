using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class NotenRechner : ContentPage
    {
        private List<string> faecher;

        public NotenRechner()
        {
            InitializeComponent();
            LadeFaecher();
        }

        private void LadeFaecher()
        {
            if (Application.Current.Properties.ContainsKey("Faecher"))
            {
                faecher = Application.Current.Properties["Faecher"] as List<string>;
            }
            else
            {
                faecher = new List<string>();
            }

            foreach (var fach in faecher)
            {
                FachZurAnsichtHinzufuegen(fach);
            }
        }

        private void SpeichereFaecher()
        {
            Application.Current.Properties["Faecher"] = faecher;
            Application.Current.SavePropertiesAsync();
        }

        private void OnAddFachClicked(object sender, EventArgs e)
        {
            string neuesFach = $"Fach {faecher.Count + 1}";
            faecher.Add(neuesFach);
            FachZurAnsichtHinzufuegen(neuesFach);
            SpeichereFaecher();
        }

        private void FachZurAnsichtHinzufuegen(string fachName)
        {
            var fachLabel = new Label
            {
                Text = fachName,
                WidthRequest = 150,
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = new Command(() => Navigation.PushAsync(new FachDetail(fachName)))
                    }
                }
            };

            FaecherStackLayout.Children.Add(fachLabel);
        }

        private void OnCalculateGesamtnoteClicked(object sender, EventArgs e)
        {
            // Logik zur Berechnung der Gesamtnote
        }
    }
}
