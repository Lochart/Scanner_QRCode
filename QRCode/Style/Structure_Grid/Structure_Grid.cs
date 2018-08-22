using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace QRCode
{
    public class Structure_Grid
    {

        #region Props

        /*
         * Summury
         * Props deel
         * Реквизит доковора
         */

        public static Grid Props(Dictionary<string, string> source)
        {
            var grid = new Grid
            {
                ColumnSpacing = 1,
                RowSpacing = 1,
                BackgroundColor = Color.Black,
                Padding = 1
            };

            var designation = new Dictionary_Designation();

            int row = 0;

            foreach (var item in source)
            {
                Debug.WriteLine("Key = " + item.Key);
                Debug.WriteLine("Value = " + item.Value);

                var response = designation.Designation.First(x => x.Key == item.Key);

                var view_Title = new StackLayout();
                view_Title.Padding = 4;

                var view_Caption = new StackLayout();
                view_Caption.Padding = 4;

                var title = Structure_Label.Custom_Label();
                title.Text = response.Value;

                var caption = Structure_Label.Custom_Label();
                caption.Text = item.Value;

                view_Title.Children.Add(title);
                view_Caption.Children.Add(caption);
                grid.Children.Add(view_Title, 0, row);
                grid.Children.Add(view_Caption, 1, row);
                row++;
            }

            return grid;
        }

        #endregion
    }
}

