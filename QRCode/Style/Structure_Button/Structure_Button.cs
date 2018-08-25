using Xamarin.Forms;

namespace QRCode
{
    public class Structure_Button 
    {
        #region Custom_Button 

        public Button Custom_Button(string text)
        {
            var button = new Button
            {
                Text = text,
                TextColor = Color.White,
                FontSize = 14,
                BackgroundColor = Color.Blue,
                CornerRadius = 25
            };

            return button;
        }

        #endregion
    }
}

