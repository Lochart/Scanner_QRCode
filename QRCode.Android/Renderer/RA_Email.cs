using Android.Content;
using Android.Text;
using QRCode.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(RA_Email))]
namespace QRCode.Droid
{
    /*
     * Реализация инфетрфейса R_Email
     */
    public class RA_Email : R_Email
    {
        public void OpenEmail(string html)
        {
            var email = new Intent(Intent.ActionSend);
            email.SetType("text/html");
            //email.PutExtra(Intent.ExtraEmail, new[]{""});

#pragma warning disable CS0618 // Тип или член устарел
            email.PutExtra(Intent.ExtraText, Html.FromHtml(html));


            Forms.Context.StartActivity(Intent.CreateChooser(
                email, "Отправить на почту...")
            );
#pragma warning restore CS0618 // Тип или член устарел
        }
    }
}

