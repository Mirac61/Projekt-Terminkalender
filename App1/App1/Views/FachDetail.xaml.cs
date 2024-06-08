using System;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class FachDetail : ContentPage
    {
        private string subjectName;

        public FachDetail(string subjectName)
        {
            InitializeComponent();
            this.subjectName = subjectName;
            SubjectNameLabel.Text = subjectName;
        }

        private void OnAddGradeClicked(object sender, EventArgs e)
        {
            // Logik zum Hinzufügen einer neuen Note
        }

        private void OnCalculateGradeClicked(object sender, EventArgs e)
        {
            // Logik zur Berechnung der Gesamtnote für dieses Fach
        }
    }
}
