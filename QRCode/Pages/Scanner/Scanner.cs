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
    public partial class Scanner : ZXingScannerPage
    {
        public Scanner()
        {
            OnScanResult += Camera;
        }

        #region Camera

        private void Camera(ZXing.Result result)
        {
            try
            {
                IsScanning = false;

                var dictionary = Function.Parsing_Text(result.Text);

                if (dictionary.Count == 0)
                    throw new Exception("Расшифровать не удалось");

                Device.BeginInvokeOnMainThread(() =>
                {
                    var next_Page = new Payment_Data(dictionary);
                    Navigation.PushAsync(next_Page);
                });
            }
            catch (Exception exception)
            {
                DisplayAlert("Внимание", exception.Message, "OK");
            }
        }

        #endregion
    }
}

