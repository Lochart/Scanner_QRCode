using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace QRCode
{
    /// <summary>
    /// Scanner page.
    /// </summary>
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new ScannerViewModel(Navigation);
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
