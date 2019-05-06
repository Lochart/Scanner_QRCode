using Xamarin.Forms;

namespace QRCode
{
    public class StructureLabel
    {
        #region Custom_Label
        public Label StyleLabel(string text)
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

