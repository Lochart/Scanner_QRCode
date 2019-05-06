using System.Collections.Generic;

namespace QRCode
{
    public static class DictionaryDesignation
    {
        public static class EnumBlock
        {
            public const string
                Service = "Служебный блок",
                Required = "Блок обязательных реквизитов",
                Additional = "Блок дополнительных реквизитов";
        }

        #region Block
        public static readonly string[] ArrayBlock = {
            EnumBlock.Service,
            EnumBlock.Required,
            EnumBlock.Additional
        };
        #endregion

        #region Service_Block
        /// <summary>
        /// The service block. - Служебный блок
        /// </summary>
        public static readonly Dictionary<string, string> ServiceBlock = new Dictionary<string, string>
        {
            {"identifierformat","Идентификатор формата"},
            {"version","Идентификатор формата"},
            {"numencodingWIN1251","Кодированный набор WIN1251"},
            {"numencodingUTF-8","Кодированный набор UTF-8"},
            {"numencodingKOI8-R","Кодированный набор KOI8-R"},
            {"sep","Разделитель реквизитов платежа"}
        };
        #endregion

        #region Required_Requisites
        /// <summary>
        /// The required requisites. - Обязательные реквизиты
        /// </summary>
        public static readonly Dictionary<string, string> RequiredRequisites = new Dictionary<string, string>
        {
            {"name","Наименование получателя платежа"},
            {"personalacc","Номер счета получателя платежа"},
            {"bankname","Номер счета получателя платежа"},
            {"bic","БИК"},
            {"correspacc","Номер кор./сч. банка получателя платежа"}
        };
        #endregion

        #region Additional_Requisites
        /// <summary>
        /// The additional requisites. - Дополнительные реквизиты
        /// </summary>
        public static readonly Dictionary<string, string> AdditionalRequisites = new Dictionary<string, string>
        {
            {"sum","Сумма платежа, в копейках"},
            {"purpose","Наименование платежа (назначение)"},
            {"payeeINN","ИНН получателя платежа"},
            {"prawerStatus","Статус составителя платежного документа"},
            {"kpp","КПП получателя платежа"},
            {"cbc","КБК"},
            {"oktmo","Общероссийский классификатор территорий муниципальных образований (ОКТМО)"},
            {"paytreason","Основание налогового платежа"},
            {"taxperiod","Налоговый период"},
            {"docno","Номер документа"},
            {"docdate","Дата документа"},
            {"taxpaytKind","Тип платежа"},
            {"lastname","Фамилия плательщика"},
            {"firstname","Имя плательщика"},
            {"Middlename","Отчество плательщика"},
            {"payeraddress","Адрес плательщика"},
            {"personalaccount","Лицевой счет бюджетного получателя"},
            {"docidx","Индекс платежного документа"},
            {"pensacc","№ лицевого счета в системе персонифицированного учета в ПФР - СНИЛС"},
            {"contract","Номер договора"},
            {"persAcc","Номер лицевого счета плательщика в организации (в системе учета ПУ)"},
            {"flat","Номер квартиры"},
            {"phone","Номер телефона"},
            {"payeridtype","Вид ДУЛ плательщика"},
            {"payeridnum","Номер ДУЛ плательщика"},
            {"childfio","Ф.И.О. ребенка/учащегося"},
            {"birthdate","Дата рождения"},
            {"paymterm","Срок платежа/дата выставления счета"},
            {"paymperiod","Период оплаты"},
            {"category","Вид платежа"},
            {"servicename","Код услуги/название прибора учета"},
            {"counterid","Номер прибора учета"},
            {"counterval","Показание прибора учета"},
            {"quittid","Номер извещения, начисления, счета"},
            {"quittdate","Дата извещения/начисления/счета/постановления (для ГИБДД)"},
            {"instnum","Номер учреждения (образовательного, медицинского)"},
            {"classnum","Номер группы детсада/класса школы"},
            {"specfio","ФИО преподавателя, специалиста, оказывающего услугу"},
            {"addamount","Сумма страховки/дополнительной услуги/Сумма пени (в копейках)"},
            {"ruleid","Номер постановления (для ГИБДД)"},
            {"execid","Номер исполнительного производства"},
            {"regtype","Код вида платежа (например, для платежей в адрес Росреестра)"},
            {"uin","Уникальный идентификатор начисления"},
            {"techcode","Технический код, рекомендуемый для заполнения поставщиком услуг." +
                " Может использоваться принимающей организацией для вызова соответствующей " +
                "обрабатывающей ИТ-системы."}
        };
        #endregion

        #region Designation
        /// <summary>
        /// The dictionary block. - Словарь обозначений псевдомимов
        /// </summary>
        public static readonly Dictionary<string, Dictionary<string, string>> DictionaryBlock = new Dictionary<string, Dictionary<string, string>>
        {
            {EnumBlock.Service, ServiceBlock},
            {EnumBlock.Required, RequiredRequisites},
            {EnumBlock.Additional, AdditionalRequisites},
        };
        #endregion
    }
}