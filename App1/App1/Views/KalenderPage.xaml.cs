
﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KalenderPage : ContentPage
    {
        public KalenderPage()
        {
            InitializeComponent(); // Initialisiert die XAML-Komponenten
        }

        public void NUllUhr_Clicked(object sender, EventArgs e)
        {
            // Hier können Sie die gewünschte Funktionalität einfügen
            DisplayAlert("Button Clicked", "Der Button für 00:00 Uhr wurde geklickt", "OK");
        }
    }
}
