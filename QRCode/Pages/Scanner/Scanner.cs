using System;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Diagnostics;

/*
 * Summury
 * Страница сканирования QRCode
 */

namespace QRCode
{
    public partial class Scanner : ContentPage
    {
        private Function function = new Function();

        private ZXingScannerView View;
        private Image Expand;

        public Scanner()
        {
            var absoluteLayout = new AbsoluteLayout();
            absoluteLayout.BackgroundColor = Color.Transparent;

            View = new ZXingScannerView
            {
                IsScanning = true,
                IsAnalyzing = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            View.OnScanResult += Camera;

            Expand = new Image
            {
                Source = "expand",
                Aspect = Aspect.AspectFill,
                HeightRequest = 200,
                WidthRequest = 200,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            AbsoluteLayout.SetLayoutFlags(View, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(View, AbsoluteLayoutFlags.SizeProportional);
            AbsoluteLayout.SetLayoutBounds(View, new Rectangle(0, 0, 1, 1));

            AbsoluteLayout.SetLayoutFlags(Expand, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(Expand, new Rectangle(0.5, 0.5, -1, -1));

            absoluteLayout.Children.Add(View);
            absoluteLayout.Children.Add(Expand);

            Content = absoluteLayout;
        }

        #region Camera

        private void Camera(ZXing.Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                var dictionary = function.Parsing_Text(result.Text);

                if (dictionary.Count != 0)
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var next_Page = new Payment_Data(dictionary);
                        Navigation.PushAsync(next_Page);
                    });
                else
                    function.Display_Alert("Внимание", "Сканирование отменено", "ОК");

            }
            else
                function.Display_Alert("Внимание", "Сканирование отменено", "ОК");
        }

        #endregion

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

