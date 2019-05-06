using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace QRCode
{
    /// <summary>
    /// Scanner view model.
    /// </summary>
    public class ScannerViewModel : Function
    {
        /// <summary>
        /// Gets or sets the result. - Результат сканирование QRCode
        /// </summary>
        /// <value>The result.</value>
        public Result Result { get; set; }

        /// <summary>
        /// Gets the QRS can result command. Событие сканирование
        /// </summary>
        /// <value>The QRS can result command.</value>
        public ICommand QRScanResultCommand { get; }

        public ScannerViewModel(INavigation navigation)
        {
            Navigation = navigation;
            QRScanResultCommand = new Command(QRScanResult);
        }

        /// <summary>
        /// QRSs the can result. - Отсканированный QRCode
        /// </summary>
        private void QRScanResult() 
        {
            Debug.WriteLine("QRScanResult");
            try
            {
                if (Result == null && string.IsNullOrEmpty(Result.Text))
                    throw new Exception("Сканирование отменено");

                var dictionary = ParsingText(GetBytesResult(Result));

                if (dictionary.Count == 0)
                    throw new Exception("Раскодировать не удалось");

                IsAnalyzing = false;
                IsScanning = false;

                OnPropertyChanged("");

                Device.BeginInvokeOnMainThread(() =>
                {
                    DictionaryData = dictionary;
                    Navigation.PushAsync(new PaymentDataPage
                    {
                        BindingContext = new PaymentDataViewModel()
                    });
                });
            }
            catch (Exception exception)
            {
                DisplayAlert("Внимание", exception.Message, "ОК");
            }

            OnPropertyChanged("");
        }
    }
}

