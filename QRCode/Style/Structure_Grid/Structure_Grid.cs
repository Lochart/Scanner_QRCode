using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace QRCode
{
    public static class Structure_Grid 
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

            int row = 0;

            row = Function.Props_Field(
                row, source, Dictionary_Designation.Service_Block, 
                Dictionary_Designation.EnumBlock.Service, grid);

            row = Function.Props_Field(
                row, source, Dictionary_Designation.Required_Requisites, 
                Dictionary_Designation.EnumBlock.Required, grid);

            row = Function.Props_Field(
                row, source, Dictionary_Designation.Additional_Requisites, 
                Dictionary_Designation.EnumBlock.Additional, grid);

            return grid;
        }

        #endregion
    }
}

