using System.Windows.Input;
using Plugin.Messaging;
using Xamarin.Forms;

namespace QRCode
{
    /// <summary>
    /// Payment data view model.
    /// </summary>
    public class PaymentDataViewModel : Function
    {
        /// <summary>
        /// Gets the events command. - Событие действий всех кнопок
        /// </summary>
        /// <value>The events command.</value>
        public ICommand EventsCommand { get; }

        public PaymentDataViewModel()
        {
            EventsCommand = new Command(EventsButton);
        }

        /// <summary>
        /// Eventses the button. Событие кнопок
        /// </summary>
        /// <param name="sender">Sender.</param>
        private void EventsButton(object sender) 
        {
            var button = sender as Button;

            // ID кнопок
            switch (button.ClassId) 
            {
                case "Email":
                    EventsSendOnEmail();
                    break;
                case "Back":
                    IsScanning = true;
                    IsAnalyzing = true;
                    OnPropertyChanged("");
                    BackNavigationPage();
                    break;
            }
        }

        /// <summary>
        /// Eventses the send on email. - Отправить на Email
        /// </summary>
        private void EventsSendOnEmail() 
        {
            var body = GetHTMLPropsTable(DictionaryData);
            var email = CrossMessaging.Current.EmailMessenger;

            if (email.CanSendEmailBodyAsHtml)
                email.SendEmail(new EmailMessageBuilder()
                    .BodyAsHtml(body)
                    .Build()
                    );
        }

        /// <summary>
        /// Creates the table. - Создание таблицы реквизита
        /// </summary>
        /// <param name="grid">Grid.</param>
        public void CreateTable(Grid grid) 
        {
            int row = 0;

            row = PropsField(row, DictionaryData, DictionaryDesignation.ServiceBlock,
                DictionaryDesignation.EnumBlock.Service, grid);

            row = PropsField(row, DictionaryData, DictionaryDesignation.RequiredRequisites,
                DictionaryDesignation.EnumBlock.Required, grid);

            row = PropsField(row, DictionaryData, DictionaryDesignation.AdditionalRequisites,
                DictionaryDesignation.EnumBlock.Additional, grid);
        }

        /// <summary>
        /// Called when page is appearing.
        /// </summary>
        public virtual void OnAppearing()
        {
            // No default implementation. 
        }

        /// <summary>
        /// Called when the view model is disappearing. View Model clean-up 
        /// should be performed here.
        /// </summary>
        public virtual void OnDisappearing()
        {
            // No default implementation. 
        }
    }
}

