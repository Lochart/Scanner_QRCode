using MessageUI;
using QRCode.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(RI_Email))]
namespace QRCode.iOS
{
    /*
    * Реализация инфетрфейса R_Email
    */
    public class RI_Email : R_Email
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

