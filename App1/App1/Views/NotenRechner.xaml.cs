using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class NotenRechner : ContentPage
    {
        private List<string> subjects;

        public NotenRechner()
        {
            InitializeComponent();
            LoadSubjects();
        }

        private void LoadSubjects()
        {
            if (Application.Current.Properties.ContainsKey("Subjects"))
            {
                subjects = Application.Current.Properties["Subjects"] as List<string>;
            }
            else
            {
                subjects = new List<string>();
            }

            foreach (var subject in subjects)
            {
                AddSubjectToView(subject);
            }
        }

        private void SaveSubjects()
        {
            Application.Current.Properties["Subjects"] = subjects;
            Application.Current.SavePropertiesAsync();
        }

        private void OnAddSubjectClicked(object sender, EventArgs e)
        {
            string newSubject = $"Fach {subjects.Count + 1}";
            subjects.Add(newSubject);
            AddSubjectToView(newSubject);
            SaveSubjects();
        }

        private void AddSubjectToView(string subjectName)
        {
            var subjectLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0)
            };

            var subjectLabel = new Label
            {
                Text = subjectName,
                WidthRequest = 150
            };

            var subjectTapGestureRecognizer = new TapGestureRecognizer();
            subjectTapGestureRecognizer.Tapped += async (s, args) =>
            {
                await Navigation.PushAsync(new FachDetail(subjectName));
            };

            subjectLayout.GestureRecognizers.Add(subjectTapGestureRecognizer);

            var deleteButton = new Button
            {
                Text = "X",
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                CornerRadius = 15,
                WidthRequest = 30,
                HeightRequest = 30,
                VerticalOptions = LayoutOptions.Center
            };

            deleteButton.Clicked += (s, args) =>
            {
                SubjectsStackLayout.Children.Remove(subjectLayout);
                subjects.Remove(subjectName);
                SaveSubjects();
            };

            subjectLayout.Children.Add(subjectLabel);
            subjectLayout.Children.Add(deleteButton);

            SubjectsStackLayout.Children.Add(subjectLayout);
        }

        private void OnCalculateGradeClicked(object sender, EventArgs e)
        {
            // Logik zur Berechnung der Gesamtnote
        }
    }
}
