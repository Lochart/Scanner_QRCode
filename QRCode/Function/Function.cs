using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace QRCode
{
    public class Function
    {
        #region Parsing_Text

        /*
         * Summury
         * Parsing text from the received QRCode
         * Парснг текста полученный из QRCode
         * 
         * char |
         * char =
         * dictionary <string, string>
         */

        public static Dictionary<string, string> Parsing_Text(string text)
        {

            char[] symbol_Vertical_Line = { '|' };
            char[] symbol_Equals = { '=' };
            var dictionary = new Dictionary<string, string>();

            string[] vertical_Line = text.Split(
                symbol_Vertical_Line, StringSplitOptions.None);
                
            if(vertical_Line.Length != 0)
                foreach (var item in vertical_Line)
                {
                    string[] equals = item.Split(symbol_Equals, StringSplitOptions.None);

                    if (equals.Length == 1)
                    {
                        /*Разделить индетификатор формата и версия формата */
                    }
                    else if (equals.Length == 2)
                        dictionary.Add(equals[0], equals[1]);
                }

            return dictionary;
        }

        #endregion
    }
}

