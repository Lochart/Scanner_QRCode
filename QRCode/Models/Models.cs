using System.Collections.Generic;

namespace QRCode
{
    /// <summary>
    /// Models. Модель объектов
    /// </summary>
    public class Models : ViewModel
    {
        /// <summary>
        /// Gets or sets the dictionary data. Словарь данных разобранного реквезита
        /// </summary>
        /// <value>The dictionary data.</value>
        public Dictionary<string, string> DictionaryData { get; set; }

        /// <summary>
        /// The is analyzing.
        /// </summary>
        private bool isAnalyzing = true;

        /// <summary>
        /// Gets or sets a value indicating whether this 
        /// <see cref="T:QRCode.Models"/> is analyzing. - Статус анализирование
        /// QRCode
        /// </summary>
        /// <value><c>true</c> if is analyzing; otherwise, <c>false</c>.</value>
        public bool IsAnalyzing
        {
            get => isAnalyzing;
            set
            {
                if (!Equals(isAnalyzing, value))
                {
                    isAnalyzing = value;
                    OnPropertyChanged(nameof(IsAnalyzing));
                }
            }
        }

        /// <summary>
        /// The is scanning.
        /// </summary>
        private bool isScanning = true;

        /// <summary>
        /// Gets or sets a value indicating whether this 
        /// <see cref="T:QRCode.Models"/> is scanning. - Статус сканирования
        /// QRCode
        /// </summary>
        /// <value><c>true</c> if is scanning; otherwise, <c>false</c>.</value>
        public bool IsScanning
        {
            get => isScanning;
            set
            {
                if (!Equals(isScanning, value))
                {
                    isScanning = value;
                    OnPropertyChanged(nameof(IsScanning));
                }
            }
        }
    }
}

