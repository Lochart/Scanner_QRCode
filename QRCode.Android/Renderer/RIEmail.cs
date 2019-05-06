using Android.Content;
using Android.Text;
using QRCode.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(RIEmail))]
namespace QRCode.Droid
{

    /// <summary>
    /// RIE mail. - Реализация инфетрфейса Email
    /// </summary>
    public class RIEmail : IEmail
    {
        public void OpenEmail(string html)
        {
            var email = new Intent(Intent.ActionSend);
            email.SetType("text/html");

#pragma warning disable CS0618 // Тип или член устарел
            email.PutExtra(Intent.ExtraText, Html.FromHtml(html));

            Forms.Context.StartActivity(Intent.CreateChooser(
                email, "Отправить на почту...")
            );
#pragma warning restore CS0618 // Тип или член устарел
        }
    }
}

