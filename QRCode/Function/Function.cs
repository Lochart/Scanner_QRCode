using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace QRCode
{
    public class Function
    {
        public void Display_Alert(
            string header, string message, string button)
        {
            DependencyService.Get<R_DisplayAlert>().ShowAlert(header, message, button);
        }

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

        public Dictionary<string, string> Parsing_Text(string text)
        {
            try
            {
                var dictionary = new Dictionary<string, string>();

                // Если текст null или состоит только из служебного блока или меньше его, то выходим
                if (text == null || text.Length <= 8)
                    return dictionary;

                var identifierFormat = text.Substring(0, 2); // Идентификатор формата

                if (identifierFormat != "ST")
                    throw new Exception("Неправильный индетификатор формата");

                var version = text.Substring(2, 4);          // Версия стандарта

                // Набор кодированных знаков, который используется для представления данных платежа.
                // Задается в виде цифрового признака кодированного набора:
                //    1 – WIN1251
                //    2 – UTF8
                //    3 – КОI8-R
                var numEncoding = text.Substring(6, 1);      // Признак набора кодированных знаков
                var sep = text.Substring(7, 1);              // Разделитель



                dictionary["IdentifierFormat"] = identifierFormat;
                dictionary["Version"] = version;
                dictionary["NumEncoding"] = numEncoding;
                dictionary["Sep"] = sep;

                // Пропускам служебный блок, оставляя поля платежа
                text = text.Substring(8);

                var items = text.Split(new[] { sep }, StringSplitOptions.None);
                foreach (var item in items)
                {
                    var pair = item.Split('=');
                    if (pair.Length == 2)
                    {
                        var field = pair[0];
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

        public int Props_Field(int row, Dictionary<string, string> source,
                                Dictionary<string, string> value, string key,
                                Grid grid)
        {
            // Заголовок блока
            Create_Header_Block_Props(row, key, grid);
            row++;

            foreach (var item in source)
            {
                var any = value.Any(x => x.Key == item.Key);

                if (any)
                {
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

        private void Create_Header_Block_Props(int row, string text, Grid grid)
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

        public string Get_HTML_Props_Table(Dictionary<string, string> source)
        {

            var block = new Dictionary_Designation();
            var body = "";

            foreach (var item in block.Dictionary_Block)
            {
                if (item.Key == "Служебный блок")
                    body += Create_HTML_Props_Table(
                        source, item.Value, item.Key);
                else if (item.Key == "Блок обязательных реквизитов")
                    body += Create_HTML_Props_Table(
                        source, item.Value, item.Key);
                else if (item.Key == "Блок дополнительных реквизитов")
                    body += Create_HTML_Props_Table(
                        source, item.Value, item.Key);
            }

            body = "<table>" + body + "</table>";
            return body;
        }

        #endregion

        #region Create_HTML_Props_Table

        /*
         * Summury
         * Create html props table
         * Содаем html реквезит таблицы
         */

        private string Create_HTML_Props_Table(
            Dictionary<string, string> source, Dictionary<string, string> value,
            string key)
        {
            // Заголовок блока
            var body = "<tr><th colspan='2'>" + key + "</th></tr>";

            foreach (var item in source)
            {
                var any = value.Any(x => x.Key == item.Key);

                if (any)
                {
                    var caption_Text = value.First(x => x.Key == item.Key).Value;

                    // Название поле и значение поле
                    string tr = "<tr><td>" + caption_Text + "</td>" +
                        "<td>" + item.Value + "</td></tr>";

                    body += tr;
                }
            }

            return body;
        }

        #endregion
    }
}

