using System;
using System.Collections.Generic;
using Xamarin.Forms;
using App1.Models;
using SQLite;

namespace App1.Views
{
    public partial class FachDetail : ContentPage
    {
        private string fachName;
        private List<double> noten;
        private User.Benutzer aktuellerBenutzer;
        private SQLiteConnection db;

        public FachDetail()
        {
            InitializeComponent();
            noten = new List<double>();
        }

        public FachDetail(string fachName, User.Benutzer aktuellerBenutzer) : this()
        {
            Console.WriteLine($"FachDetail wird geladen für Fach: {fachName} und Benutzer: {aktuellerBenutzer.Benutzername}");
            this.fachName = fachName ?? throw new ArgumentNullException(nameof(fachName));
            this.aktuellerBenutzer = aktuellerBenutzer ?? throw new ArgumentNullException(nameof(aktuellerBenutzer));

            FachNameLabel.Text = fachName;
            User.InitialisiereDatenbank(aktuellerBenutzer.Benutzername);
            db = User.GetDatenbank();
            LadeNoten();
        }

        private void LadeNoten()
        {
            try
            {
                var notenListe = db.Table<User.Benutzer>().Where(n => n.Benutzername == aktuellerBenutzer.Benutzername && n.FachName == fachName).ToList();
                noten.Clear();
                foreach (var note in notenListe)
                {
                    noten.Add(note.Note);
                    ZeigeNoteEintrag(note.Note);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", "Laden der Noten fehlgeschlagen: " + ex.Message, "OK");
            }
        }

        private void SpeichereNoten()
        {
            try
            {
                db.Table<User.Benutzer>().Delete(n => n.Benutzername == aktuellerBenutzer.Benutzername && n.FachName == fachName);
                foreach (var note in noten)
                {
                    db.Insert(new User.Benutzer { FachName = fachName, Note = note, Benutzername = aktuellerBenutzer.Benutzername });
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", "Speichern der Noten fehlgeschlagen: " + ex.Message, "OK");
            }
        }

        private void OnAddNoteClicked(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(NoteEintrag.Text, out double neueNote))
                {
                    noten.Add(neueNote);
                    ZeigeNoteEintrag(neueNote);
                    SpeichereNoten();
                    NoteEintrag.Text = "";
                }
                else
                {
                    DisplayAlert("Fehler", "Bitte eine gültige Note eingeben", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", "Beim Hinzufügen der Note ist ein Fehler aufgetreten: " + ex.Message, "OK");
            }
        }

        private void ZeigeNoteEintrag(double note)
        {
            try
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
                        if (index >= 0 && index < noten.Count)
                        {
                            noten[index] = geaenderteNote;
                            SpeichereNoten();
                        }
                    }
                    else
                    {
                        DisplayAlert("Fehler", "Bitte eine gültige Note eingeben", "OK");
                    }
                };

                NotenStackLayout.Children.Add(noteEntry);
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", "Beim Hinzufügen der Note zur Ansicht ist ein Fehler aufgetreten: " + ex.Message, "OK");
            }
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
