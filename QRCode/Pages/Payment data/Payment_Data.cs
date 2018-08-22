using System;
using Xamarin.Forms;
using System.Collections.Generic;

/*
 * Summury
 * Страница Реквезит сделки
 */

namespace QRCode
{
    public class Payment_Data : ContentPage
    {
        private Dictionary<string, string> dictionary;

        public Payment_Data(Dictionary<string, string> dictionary)
        {
            NavigationPage.SetHasBackButton(this, false);

            this.dictionary = dictionary;

            var View = new StackLayout{
                Padding = 16,
                Spacing = 16
            };

            var Grid = Structure_Grid.Props(dictionary);

            View.Children.Add(Grid);

            var Scroll = new ScrollView();
            Scroll.Content = View;

            Content = Scroll;
        }
    }
}

