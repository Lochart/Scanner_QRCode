using System;
using Xamarin.Forms;

/*
 * Summury
 * Страница Реквезит сделки
 */

namespace QRCode
{
    public class Payment_Data : ContentPage
    {
        public Payment_Data()
        {
            NavigationPage.SetHasBackButton(this, false);

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };


        }
    }


}

