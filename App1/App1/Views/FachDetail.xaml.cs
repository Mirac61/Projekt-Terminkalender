using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class FachDetail : ContentPage
    {
        private string fachName;
        private List<double> noten;

        public FachDetail(string fachName)
        {
            InitializeComponent();
            this.fachName = fachName;
            FachNameLabel.Text = fachName;
            noten = new List<double>();
            LadeNoten();
        }

        private void LadeNoten()
        {
            if (Application.Current.Properties.ContainsKey(fachName))
            {
                noten = Application.Current.Properties[fachName] as List<double>;
            }
            else
            {
                noten = new List<double>();
            }

            foreach (var note in noten)
            {
                NoteZurAnsichtHinzufuegen(note);
            }
        }

        private void SpeichereNoten()
        {
            Application.Current.Properties[fachName] = noten;
            Application.Current.SavePropertiesAsync();
        }

        private void OnAddNoteClicked(object sender, EventArgs e)
        {
            if (double.TryParse(NoteEintrag.Text, out double neueNote))
            {
                noten.Add(neueNote);
                NoteZurAnsichtHinzufuegen(neueNote);
                SpeichereNoten();
                NoteEintrag.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Fehler", "Bitte eine gültige Note eingeben", "OK");
            }
        }

        private void NoteZurAnsichtHinzufuegen(double note)
        {
            var noteEntry = new Entry
            {
                Text = note.ToString("F2"),
                WidthRequest = 150,
                Keyboard = Keyboard.Numeric
            };

            noteEntry.Completed += (s, e) =>
            {
                if (double.TryParse(noteEntry.Text, out double geaenderteNote))
                {
                    int index = NotenStackLayout.Children.IndexOf(noteEntry);
                    noten[index] = geaenderteNote;
                    SpeichereNoten();
                }
                else
                {
                    DisplayAlert("Fehler", "Bitte eine gültige Note eingeben", "OK");
                }
            };

            NotenStackLayout.Children.Add(noteEntry);
        }

        private void OnCalculateNoteClicked(object sender, EventArgs e)
        {
            if (noten.Count == 0)
            {
                DisplayAlert("Fehler", "Keine Noten vorhanden", "OK");
                return;
            }

            double gesamt = 0;
            foreach (var note in noten)
            {
                gesamt += note;
            }

            double durchschnitt = gesamt / noten.Count;
            DisplayAlert("Gesamtnote", $"Die Gesamtnote für {fachName} ist {durchschnitt:F2}", "OK");
        }
    }
}
