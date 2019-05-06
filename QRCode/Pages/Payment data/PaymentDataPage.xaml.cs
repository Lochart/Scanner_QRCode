using Xamarin.Forms;

namespace QRCode
{
    /// <summary>
    /// Payment data page.
    /// </summary>
    public partial class PaymentDataPage : ContentPage
    {
        public PaymentDataPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var viewModel = BindingContext as PaymentDataViewModel;

            // Inform the view model that it is disappearing so that it can remove event handlers
            // and perform any other clean-up required..
            viewModel?.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as PaymentDataViewModel;

            viewModel.CreateTable(Table);
            // Inform the view model that it is appearing.
            viewModel?.OnAppearing();
        }
    }
}
