﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using ZXing;

namespace QRCode
{
    public static class Function
    {
        public static void Display_Alert(
            string header, string message, string button)
        {
            DependencyService.Get<R_DisplayAlert>().ShowAlert(header, message, button);
        }

        #region Get_Bytes_Result

        /*
        * Summury
        * Получения массива байтов из ZXing.Result.
        */

        public static byte[] Get_Bytes_Result(Result result)
       {
           var byteSegments = (IList<byte[]>)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS];

           int totalLength = 0;
           foreach (byte[] bs in byteSegments)
           {
               totalLength += bs.Length;
           }

           byte[] resultBytes = new byte[totalLength];
           int i = 0;
           foreach (byte[] bs in byteSegments)
           {
               foreach (byte b in bs)
               {
                   resultBytes[i++] = b;
               }
           }

           return resultBytes;
       }

       #endregion

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
        public static Dictionary<string, string> Parsing_Text(byte[] bytes)
        {
            try
            {

                var utf8 = Encoding.GetEncoding("utf-8");
                var win1251 = Encoding.GetEncoding("windows-1251");
                var koi8 = Encoding.GetEncoding("koi8-r");

                var num_Byte = bytes[6];
                // Набор кодированных знаков, который используется для представления данных платежа.
                // Задается в виде цифрового признака кодированного набора:
                //    1 – WIN1251
                //    2 – UTF8
                //    3 – КОI8-R
                const byte IS_WIN1251 = 49; // "1" в байтах это код 49
                const byte IS_UTF8 = 50; // "2" в байтах это код 50
                const byte IS_КОI8_R = 51; // "3" в байтах это код 51

                string text;

                switch (num_Byte)
                {
                    case IS_WIN1251:
                        text = win1251.GetString(bytes);
                        break;
                    case IS_UTF8:
                        text = utf8.GetString(bytes);
                        break;
                    case IS_КОI8_R:
                        text = koi8.GetString(bytes);
                        break;
                    default:
                        throw new Exception("Неправильный формат!");
                }

                var dictionary = new Dictionary<string, string>();

                // Если текст null или состоит только из служебного блока или меньше его, то выходим
                if (string.IsNullOrEmpty(text) || text.Length <= 8)
                    return dictionary;

                var identifierFormat = text.Substring(0, 2); // Идентификатор формата

                if (identifierFormat != "ST")
                    throw new Exception("Неправильный индетификатор формата");

                var version = text.Substring(2, 4); // Версия стандарта

                var numEncoding = text.Substring(6, 1);  // Признак набора кодированных знаков

                var sep = text.Substring(7, 1); // Разделитель

                dictionary["identifierformat"] = identifierFormat;
                dictionary["version"] = version;
                dictionary["numencoding_" + numEncoding] = numEncoding;
                dictionary["sep"] = sep;

                // Пропускам служебный блок, оставляя поля платежа
                text = text.Substring(8);

                var items = text.Split(new[] { sep }, StringSplitOptions.None);
                foreach (var item in items)
                {
                    var pair = item.Split('=');
                    if (pair.Length == 2)
                    {
                        var field = pair[0].ToLower();
                        var value = pair[1];

                        dictionary[field] = value;
                    }
                }

                return dictionary;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Внимание " + exception.Message);
                return null;
            }
        }

        #endregion

        #region Props_Field

        /*
         * Summury
         * Add props field in grid
         * Добавление реквезитов полей в таблицу
         */
        public static int Props_Field(int row, Dictionary<string, string> source,
                                Dictionary<string, string> value, string key,
                                Grid grid)
        {
            // Заголовок блока
            Create_Header_Block_Props(row, key, grid);
            row++;

            foreach (var item in source)
            {
                // Если такого поля нет в результатах
                if (!value.ContainsKey(item.Key))
                    continue;

                var row_Definition = new RowDefinition();
                row_Definition.Height = new GridLength(1, GridUnitType.Auto);
                grid.RowDefinitions.Add(row_Definition);

                var view_Title = new StackLayout();
                view_Title.BackgroundColor = Color.White;
                view_Title.Padding = 4;

                var view_Caption = new StackLayout();
                view_Caption.BackgroundColor = Color.White;
                view_Caption.Padding = 4;

                var text = value.First(x => x.Key == item.Key);

                var label = new Structure_Label();

                // Название поле 
                var title = label.Custom_Label(text.Value);

                // Значение поле 
                var caption = label.Custom_Label(item.Value);

                view_Title.Children.Add(title);
                view_Caption.Children.Add(caption);
                grid.Children.Add(view_Title, 0, row);
                grid.Children.Add(view_Caption, 1, row);
                row++;
            }

            return row;
        }

        #endregion

        #region Create_Header_Block_Props

        /*
         * Summury
         * Create view header block зrops
         * Содаем вид заголовка блока реквезита
         */
        private static void Create_Header_Block_Props(int row, string text, Grid grid)
        {
            var view_Header = new StackLayout();
            view_Header.BackgroundColor = Color.White;
            view_Header.Padding = 4;

            var label = new Structure_Label();

            var header = label.Custom_Label(text);
            header.HorizontalTextAlignment = TextAlignment.Center;

            view_Header.Children.Add(header);
            grid.Children.Add(view_Header, 0, row);
            Grid.SetColumnSpan(view_Header, 2); // обьединяем две ячейки

            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Auto)
            });
        }

        #endregion

        #region Get_HTML_Props_Table

        /*
         * Summury
         * Get html props table
         * Получаем html реквезит таблицы
         */
        public static string Get_HTML_Props_Table(Dictionary<string, string> source)
        {
            var body = "";

            foreach (var item in Dictionary_Designation.Dictionary_Block)
                body += Create_HTML_Props_Table(source, item.Key, item.Value);

            body = "<table> " + body + " </table>";
            Debug.WriteLine(body);
            return body;
        }

        #endregion

        #region Create_HTML_Props_Table

        /*
         * Summury
         * Create html props table
         * Содаем html реквезит таблицы
         */
        private static string Create_HTML_Props_Table(
            Dictionary<string, string> source, string key,
            Dictionary<string, string> value)
        {
            // Заголовок блока
            var body = "<tr><th colspan='2'> " + key + " </th></tr>\n";

            foreach (var item in source)
            {
                if (!value.ContainsKey(item.Key))
                    continue;

                var caption_Text = value[item.Key];

                // Название поле и значение поле
                string tr = "<tr><td> " + caption_Text + " </td>" +
                    "<td> " + item.Value + " </td></tr>\n";

                body += tr;
            }

            return body;
        }

        #endregion
    }
}

