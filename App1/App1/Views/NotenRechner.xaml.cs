using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class NotenRechner : ContentPage
    {
        // Dictionary zur Speicherung der Noten nach Fachnamen
        private Dictionary<string, List<double>> faecherNoten;

        public NotenRechner()
        {
            InitializeComponent();
            faecherNoten = new Dictionary<string, List<double>>();
        }

        // Event-Handler für das Hinzufügen einer Note
        private void NoteHinzufuegen(object sender, EventArgs e)
        {
            if (!double.TryParse(noteEntry.Text, out double note))
            {
                DisplayAlert("Fehler", "Bitte eine gültige Note eingeben", "OK");
                return;
            }

            var fachName = fachNameEntry.Text;
            if (string.IsNullOrWhiteSpace(fachName))
            {
                DisplayAlert("Fehler", "Bitte einen Fachnamen eingeben", "OK");
                return;
            }

            if (!faecherNoten.ContainsKey(fachName))
            {
                faecherNoten[fachName] = new List<double>();
                var cell = new TextCell { Text = fachName, Detail = $"Durchschnittsnote: {note:F2}" };
                cell.AutomationId = fachName; // Setze eine eindeutige ID für die Zelle
                faecherSection.Add(cell);
            }

            faecherNoten[fachName].Add(note);
            AktualisiereFach(fachName);

            noteEntry.Text = string.Empty;
        }

        // Event-Handler für das Speichern des Fachs und der Noten
        private void FachUndNotenSpeichern(object sender, EventArgs e)
        {
            var fachName = fachNameEntry.Text;
            if (string.IsNullOrWhiteSpace(fachName) || !faecherNoten.ContainsKey(fachName))
            {
                DisplayAlert("Fehler", "Bitte einen Fachnamen und mindestens eine Note eingeben", "OK");
                return;
            }

            double durchschnitt = BerechneDurchschnitt(fachName);

            // Erstelle eine neue Zeile für die Tabelle mit Fachname und Durchschnittsnote
            for (int i = 0; i < faecherSection.Count; i++)
            {
                var cell = faecherSection[i];
                if (cell is TextCell textCell && textCell.AutomationId == fachName)
                {
                    textCell.Detail = $"Durchschnittsnote: {durchschnitt:F2}";
                    break;
                }
            }

            // Felder zurücksetzen
            fachNameEntry.Text = string.Empty;
            notenSection.Clear();
        }

        // Methode zur Aktualisierung der Noten eines Fachs
        private void AktualisiereFach(string fachName)
        {
            double durchschnitt = BerechneDurchschnitt(fachName);

            for (int i = 0; i < faecherSection.Count; i++)
            {
                var cell = faecherSection[i];
                if (cell is TextCell textCell && textCell.AutomationId == fachName)
                {
                    textCell.Detail = $"Durchschnittsnote: {durchschnitt:F2}";
                    break;
                }
            }
        }

        // Methode zur Berechnung des Durchschnitts der Noten eines bestimmten Fachs
        private double BerechneDurchschnitt(string fachName)
        {
            var noten = faecherNoten[fachName];
            double summe = 0;
            for (int i = 0; i < noten.Count; i++)
            {
                summe += noten[i];
            }
            return summe / noten.Count;
        }
    }
}
