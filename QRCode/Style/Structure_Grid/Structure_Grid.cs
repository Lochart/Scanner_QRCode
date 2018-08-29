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

            foreach (var item in dictionary.Dictionary_Block)
            {
                if (item.Key == "Служебный блок")
                    row = Props_Field(row, source, item.Value, item.Key, grid);
                else if (item.Key == "Блок обязательных реквизитов")
                    row = Props_Field(row, source, item.Value, item.Key, grid);
                else if(item.Key == "Блок дополнительных реквизитов")
                    row = Props_Field(row, source, item.Value, item.Key, grid);
            }

            return grid;
        }

        #endregion
    }
}

