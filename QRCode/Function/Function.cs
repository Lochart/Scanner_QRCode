using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using ZXing;

namespace QRCode
{
    public class Function : Models
    {
        /// <summary>
        /// Displaies the alert.
        /// </summary>
        /// <param name="header">Header.</param>
        /// <param name="message">Message.</param>
        /// <param name="button">Button.</param>
        public void DisplayAlert(string header, string message, string button) => 
            DependencyService.Get<IDisplayAlert>().ShowAlert(header, message, button);
            
        /// <summary>
        /// Gets the bytes result. - Получение массива байтов из ZXing.Result.
        /// </summary>
        /// <returns>The bytes result.</returns>
        /// <param name="result">Result.</param>
        public byte[] GetBytesResult(Result result)
        {
            var byteSegments = (IList<byte[]>)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS];

            int totalLength = 0;

            foreach (byte[] bs in byteSegments)
                totalLength += bs.Length;

            byte[] resultBytes = new byte[totalLength];

            int i = 0;

            foreach (byte[] bs in byteSegments)
                foreach (byte b in bs)
                    resultBytes[i++] = b;

            return resultBytes;
        }

        /// <summary>
        /// Parsings the text. - Парснг текста полученный из QRCode
        /// </summary>
        /// <returns>dictionary</returns>
        /// <param name="bytes">Bytes.</param>
        public Dictionary<string, string> ParsingText(byte[] bytes)
        {
            try
            {            
                var numberByte = bytes[6];
                // Набор кодированных знаков, который используется для представления данных платежа.
                // Задается в виде цифрового признака кодированного набора:
                //    1 – WIN1251
                //    2 – UTF8
                //    3 – КОI8-R
                const byte IS_WIN1251 = 49; // "1" в байтах это код 49
                const byte IS_UTF8 = 50; // "2" в байтах это код 50
                const byte IS_КОI8_R = 51; // "3" в байтах это код 51

                string text;
                string numencoding;

                switch (numberByte)
                {
                    case IS_WIN1251:
						var win1251 = Encoding.GetEncoding("windows1251");
                        text = win1251.GetString(bytes);
                        numencoding = "numencodingWIN1251";
                        break;
                    case IS_UTF8:
                        var utf8 = Encoding.GetEncoding("utf8");
                        text = utf8.GetString(bytes);
                        numencoding = "numencodingUTF-8";
                        break;
                    case IS_КОI8_R:
						var koi8 = Encoding.GetEncoding("koi8r");
                        text = koi8.GetString(bytes);
                        numencoding = "numencodingKOI8-R";
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
                dictionary[numencoding] = numEncoding;
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

        /// <summary>
        /// Propertieses the field. - Реквезит полей
        /// </summary>
        /// <returns>The field.</returns>
        /// <param name="row">Row.</param>
        /// <param name="source">Source.</param>
        /// <param name="value">Value.</param>
        /// <param name="key">Key.</param>
        /// <param name="grid">Grid.</param>
        public int PropsField(int row, Dictionary<string, string> source,
            Dictionary<string, string> value, string key, Grid grid)
        {
            // Заголовок блока
            CreateHeaderBlockProps(row, key, grid);
            row++;

            foreach (var item in source)
            {
                // Если такого поля нет в результатах
                if (!value.ContainsKey(item.Key))
                    continue;

                var row_Definition = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                };
                grid.RowDefinitions.Add(row_Definition);

                var view_Title = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Padding = 4
                };

                var view_Caption = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Padding = 4
                };

                var text = value.First(x => x.Key == item.Key);

                var label = new StructureLabel();

                view_Title.Children.Add(label.StyleLabel(text.Value)); // Название полей 
                view_Caption.Children.Add(label.StyleLabel(item.Value)); // Значение поля
                grid.Children.Add(view_Title, 0, row);
                grid.Children.Add(view_Caption, 1, row);
                row++;
            }

            return row;
        }

        /// <summary>
        /// Creates the header block properties. - Создаем вид заголовка блока 
        /// реквезита
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="text">Text.</param>
        /// <param name="grid">Grid.</param>
        private static void CreateHeaderBlockProps(int row, string text, Grid grid)
        {
            var viewHeader = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = 4
            };

            var label = new StructureLabel();

            var header = label.StyleLabel(text);
            header.HorizontalTextAlignment = TextAlignment.Center;

            viewHeader.Children.Add(header);
            grid.Children.Add(viewHeader, 0, row);
            Grid.SetColumnSpan(viewHeader, 2);

            grid.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Auto)
            });
        }

        /// <summary>
        /// Gets the HTMLP rops table. - Получаем html реквезит таблицы
        /// </summary>
        /// <returns>The HTMLP rops table.</returns>
        /// <param name="source">Source.</param>
        public string GetHTMLPropsTable(Dictionary<string, string> source)
        {
            var body = "<table>";

            foreach (var item in DictionaryDesignation.DictionaryBlock)
            {
                var value = item.Value;

                body += "<tr><th colspan='2'> " + item.Key + " </th></tr>\n";

                foreach (var field in source)
                {
                    if (!value.ContainsKey(field.Key))
                        continue;

                    // Название поле и значение поле
                    body += string.Format(
                        "<tr><td> {0}  </td><td> {1} </td></tr>\n",
                        value[field.Key], field.Value);
                }
            }

            body += "</table>";
            Debug.WriteLine(body);
            return body;
        }
    }
}

