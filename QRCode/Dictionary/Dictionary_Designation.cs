using System.Collections.Generic;

namespace QRCode
{
    public class Dictionary_Designation 
    {
        #region Designation

        /*
         * Summury 
         * Dictionary designation aliases
         * Словарь обозначений псевдомимов
         */

        public Dictionary<string, Dictionary<string, string>> Dictionary_Block;

        public Dictionary_Designation()
        {
            Dictionary_Block = new Dictionary<string, Dictionary<string, string>>();

            var service = new Dictionary_Service_Block();
            var required = new Dictionary_Required_Requisites();
            var additional = new Dictionary_Additional_Requisites();

            Dictionary_Block.Add("Служебный блок", service.Service_Block);

            Dictionary_Block.Add("Блок обязательных реквизитов", 
                                 required.Required_Requisites);

            Dictionary_Block.Add("Блок дополнительных реквизитов", 
                                 additional.Additional_Requisites);

        }

        #endregion
    }
}

