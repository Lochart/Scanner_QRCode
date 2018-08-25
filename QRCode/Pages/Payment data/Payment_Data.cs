using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Plugin.Messaging;

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

            var View_Panel_Button = new StackLayout
            {
                Spacing = 16,
                Orientation = StackOrientation.Horizontal 
            };

            var structure_Grid = new Structure_Grid();
            var Grid = structure_Grid.Props(dictionary);

            var button = new Structure_Button();
            var Back = button.Custom_Button("Назад");

            //var Back = new Button { 
            //    Text = "Назад",
            //    TextColor = Color.White,
            //    FontSize = 14,
            //    BackgroundColor = Color.Blue,
            //    CornerRadius = 25
            //};
            Back.Clicked += Back_Page;


            var Send_Email = button.Custom_Button("Отправить на email");
            //var Send_Email = new Button
            //{
            //    Text = "Отправить на email",
            //    TextColor = Color.White,
            //    FontSize = 14,
            //    BackgroundColor = Color.Blue,
            //    CornerRadius = 25
            //};
            Send_Email.Clicked += Clicked_Send_Email;

            View_Panel_Button.Children.Add(Back);
            View_Panel_Button.Children.Add(Send_Email);

            View.Children.Add(Grid);
            View.Children.Add(View_Panel_Button);

            var Scroll = new ScrollView();
            Scroll.Content = View;

            Content = Scroll;
        }

        private async void Back_Page(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Clicked_Send_Email(object sender, EventArgs e)
        {
            var function = new Function();
            var body = function.Get_HTML_Props_Table(dictionary);

            /*
             * Summury
             * Реализация интерфейса Email
             */
            //var email = DependencyService.Get<R_Email>();
            //email.OpenEmail(body); // Открыть почту


            /*
             * Summury
             * Библиотека Xam.Plugins.Messaging
             */
            var email = CrossMessaging.Current.EmailMessenger;

            if (email.CanSendEmailBodyAsHtml)
            {
                var message = new EmailMessageBuilder().BodyAsHtml(body).Build();

                email.SendEmail(message);
            }
        }
    }
}

