using MessageUI;
using QRCode.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(RIEmail))]
namespace QRCode.iOS
{
    /// <summary>
    /// RIE mail. - Реализация инфетрфейса Email
    /// </summary>
    public class RIEmail : IEmail
    {
        public void OpenEmail(string html)
        {
            var email = new MFMailComposeViewController();
            email.SetMessageBody(html, true);
            email.Finished += (object sender, MFComposeResultEventArgs e) => {
                e.Controller.DismissViewController(true, null);
            };

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(
                email, true, null);

        }
    }
}

