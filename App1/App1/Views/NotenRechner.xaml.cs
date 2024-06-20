using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class NotenRechner : ContentPage
    {
        // Liste zur Speicherung der Fächer und ihrer Noten
        private List<Fach> faecherListe;

        // Konstruktor, der die Seite initialisiert und die Liste der Fächer erstellt
        public NotenRechner()
        {
            InitializeComponent(); // Lädt die XAML-Komponenten und initialisiert die Seite.
            faecherListe = new List<Fach>(); // Initialisiert die leere Liste der Fächer.
        }

        // Event-Handler für das Hinzufügen einer Note
        private void NoteHinzufuegen(object sender, EventArgs e)
        {
            // Versucht, die eingegebene Note in ein Double zu konvertieren
            if (!double.TryParse(noteEntry.Text, out double note) || string.IsNullOrWhiteSpace(fachNameEntry.Text))
            {
                // Zeigt eine Fehlermeldung an, wenn die Eingabe ungültig ist
                DisplayAlert("Fehler", "Bitte einen gültigen Fachnamen und eine gültige Note eingeben", "OK");
                return; // Beendet die Methode, wenn die Eingabe ungültig ist.
            }

            // Speichert den eingegebenen Fachnamen
            string fachName = fachNameEntry.Text;

            // Sucht nach dem Fach in der Liste der Fächer
            Fach fach = GetFachByName(fachName);

            // Wenn das Fach nicht gefunden wird, wird es neu erstellt und zur Liste hinzugefügt
            if (fach == null)
            {
                fach = new Fach(fachName); // Erstellt ein neues Fach mit dem eingegebenen Namen.
                faecherListe.Add(fach); // Fügt das neue Fach zur Liste der Fächer hinzu.
                // Fügt eine neue Zelle zur Anzeige des Fachs und der Note hinzu
                faecherSection.Add(new TextCell { Text = fachName, Detail = $"Durchschnittsnote: {note:F2}", AutomationId = fachName });
            }

            // Fügt die neue Note zum Fach hinzu
            fach.Noten.Add(note);
            // Aktualisiert die Anzeige für das Fach
            AktualisiereFach(fachName);

            // Setzt das Eingabefeld für die Note zurück
            noteEntry.Text = string.Empty;
        }

        // Methode zur Aktualisierung der Noten eines Fachs mithilfe von ChatGPT
        private void AktualisiereFach(string fachName)
        {
            // Sucht nach dem Fach in der Liste der Fächer
            Fach fach = GetFachByName(fachName);
            if (fach == null) return; // Wenn das Fach nicht gefunden wird, beendet die Methode.

            // Berechnet den Durchschnitt der Noten für das Fach
            double durchschnitt = fach.BerechneDurchschnitt();

            // Aktualisiert die Anzeige der Durchschnittsnote für das Fach in der Tabelle
            for (int i = 0; i < faecherSection.Count; i++)
            {
                // Findet die Zelle für das Fach und aktualisiert die Detail-Anzeige
                if (faecherSection[i] is TextCell textCell && textCell.AutomationId == fachName)
                {
                    textCell.Detail = $"Durchschnittsnote: {durchschnitt:F2}";
                    break; // Beendet die Schleife nach dem Update.
                }
            }
        }

        // Methode zur Suche nach einem Fach in der Liste der Fächer
        private Fach GetFachByName(string fachName)
        {
            for (int i = 0; i < faecherListe.Count; i++)
            {
                if (faecherListe[i].Name == fachName)
                {
                    return faecherListe[i];
                }
            }
            return null;
        }
    }

    // Klasse zur Darstellung eines Fachs mit einer Liste von Noten
    public class Fach
    {
        // Eigenschaften für den Namen des Fachs und die Liste der Noten
        public string Name { get; set; }
        public List<double> Noten { get; set; }

        // Konstruktor, der den Namen initialisiert und die Liste der Noten erstellt
        public Fach(string name)
        {
            Name = name;
            Noten = new List<double>();
        }

        // Berechnet den Durchschnitt der Noten
        public double BerechneDurchschnitt()
        {
            if (Noten.Count == 0) return 0; // Wenn keine Noten vorhanden sind, gib 0 zurück.

            double summe = 0;
            // Addiert alle Noten
            for (int i = 0; i < Noten.Count; i++)
            {
                summe += Noten[i];
            }
            // Berechnet den Durchschnitt und gibt ihn zurück
            return summe / Noten.Count;
        }
    }

}
