using QRCode.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(RI_DisplayAlert))]
namespace QRCode.iOS
{
    /*
     * Реализация инфетрфейса R_DisplayAlert
     */
    public class RI_DisplayAlert : R_DisplayAlert
    {
        public void ShowAlert(string title, string content, string buttonText)
        {
            UIAlertView alert = new UIAlertView
            {
                Title = title,
                Message = content
            };
            alert.AddButton(buttonText);
            alert.Show();
        }
    }

}