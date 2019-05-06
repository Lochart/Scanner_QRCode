using System.ComponentModel;
using Xamarin.Forms;

namespace QRCode
{
    /// <summary>
    /// View model.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; set; }

        public ViewModel()
        {
        }

        /// <summary>
        /// Backs the navigation page. Вернуться на предыдущую страницу
        /// </summary>
        public void BackNavigationPage() => Navigation.PopAsync();

        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(
            this, new PropertyChangedEventArgs(propName));

        public virtual void Disponse() { }
    }
}

