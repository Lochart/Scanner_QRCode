using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace QRCode
{
    public class Structure_Grid : Function
    {

        #region Props

        /*
         * Summury
         * Props deel
         * Реквизит доковора
         */
        public Grid Props(Dictionary<string, string> source)
        {
            var grid = new Grid
            {
                ColumnSpacing = 1,
                RowSpacing = 1,
                BackgroundColor = Color.Black,
                Padding = 1
            };

            int row = 0;

            var dictionary = new Dictionary_Designation();

            foreach (var title in dictionary.Array_Block)
                row = Props_Field(
                    row, source, dictionary.Dictionary_Block[title], title, grid);
           
            return grid;
        }

        #endregion
    }
}

