using System;

using Xamarin.Forms;

namespace QRCode
{
    public class Structure_Label
    {
        #region Custom_Label

        public static Label Custom_Label()
        {
            var label = new Label
            {
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = 14
            };

            return label;
        }

        #endregion
    }
}

