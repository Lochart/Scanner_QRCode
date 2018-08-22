using System;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing;

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
            NavigationPage.SetHasNavigationBar(this, false);

            OnScanResult += Camera;
        }

        private void Camera (Result result){
            // Parar de escanear
            IsScanning = false;

            // Alert com o código escaneado
            Device.BeginInvokeOnMainThread(() => {

                var dictionary = Function.Parsing_Text(result.Text);

                //Function.Parsing_Text();

                //var next_Page = new Payment_Data();


                //Navigation.PushAsync(next_Page);

                //DisplayAlert("Código escaneado", result.Text, "OK");
            });
        }
    }
}

