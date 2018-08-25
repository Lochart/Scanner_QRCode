using System.Collections.Generic;

namespace QRCode
{
    public class Dictionary_Required_Requisites
    {
        #region Required_Requisites

        /*
         * Summury 
         * Required_Requisites
         * Обязательные реквизиты
         */

        public Dictionary<string, string> Required_Requisites = new Dictionary<string, string>
        {
            {"Name","Наименование получателя платежа"},
            {"PersonalAcc","Номер счета получателя платежа"},
            {"BankName","Номер счета получателя платежа"},
            {"BIC","БИК"},
            {"CorrespAcc","Номер кор./сч. банка получателя платежа"}
        };

        #endregion
    }
}

