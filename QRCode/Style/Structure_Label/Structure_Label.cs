using System;

using Xamarin.Forms;

namespace QRCode
{
    public class Structure_Label
    {
        #region Custom_Label

        public Label Custom_Label(string text)
        {
            var label = new Label
            {
                Text = text,
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            return label;
        }

        #endregion
    }
}

