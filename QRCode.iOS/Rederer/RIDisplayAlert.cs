using QRCode.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(RIDisplayAlert))]
namespace QRCode.iOS
{
    /// <summary>
    /// RID isplay alert. - Реализация инфетрфейса DisplayAlert
    /// </summary>
    public class RIDisplayAlert : IDisplayAlert
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