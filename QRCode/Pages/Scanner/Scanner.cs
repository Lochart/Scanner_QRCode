using System;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Diagnostics;
using ZXing;
using System.Collections.Generic;

/*
 * Summury
 * Страница сканирования QRCode
 */

namespace QRCode
{
    public partial class Scanner : ContentPage
    {
        private Function function = new Function();

        private ZXingScannerView ScannerView;
        private Image Expand;
        private AbsoluteLayout absoluteLayout;

        public Scanner()
        {
            absoluteLayout = new AbsoluteLayout();
            absoluteLayout.BackgroundColor = Color.Transparent;

            ScannerView = new ZXingScannerView
            {
                IsScanning = true,
                IsAnalyzing = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            ScannerView.OnScanResult += Camera;

            Expand = new Image
            {
                Source = "expand",
                Aspect = Aspect.AspectFill,
                HeightRequest = 200,
                WidthRequest = 200,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            AbsoluteLayout.SetLayoutFlags(ScannerView, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(ScannerView, AbsoluteLayoutFlags.SizeProportional);
            AbsoluteLayout.SetLayoutBounds(ScannerView, new Rectangle(0, 0, 1, 1));

            AbsoluteLayout.SetLayoutFlags(Expand, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(Expand, new Rectangle(0.5, 0.5, -1, -1));


        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Debug.WriteLine("OnDisappearing");
            ScannerView.IsScanning = false;
            ScannerView.IsAnalyzing = false;
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("OnAppearing");
            Scanning_Analyzing();
            base.OnAppearing();
 

        }

        private void Scanning_Analyzing()
        {
            Debug.Write("1");
            ScannerView.IsScanning = true;
            ScannerView.IsAnalyzing = true;

            absoluteLayout.Children.Clear();
            absoluteLayout.Children.Add(ScannerView);
            absoluteLayout.Children.Add(Expand);

            Content = absoluteLayout;
        }

        #region Camera

        private void Camera(ZXing.Result result)
        {
            try
            {
                if (result == null && string.IsNullOrEmpty(result.Text))
                    throw new Exception("Сканирование отменено");

                var byteSegments = (IList<byte[]>)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS];

                var dictionary = function.Parsing_Text(byteSegments);

                if (dictionary.Count == 0)
                    throw new Exception("Раскодировать не удалось");

                ScannerView.IsScanning = false;
                ScannerView.IsAnalyzing = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var next_Page = new Payment_Data(dictionary);
                    Navigation.PushAsync(next_Page);
                });
            }
            catch (Exception exception)
            {
                function.Display_Alert("Внимание", exception.Message, "ОК");
            }
        }

        #endregion
    }
}

