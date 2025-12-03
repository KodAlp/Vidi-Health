using Vidi_Health.Pages;
namespace Vidi_Health
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            // UserRegistrationPage'e git
            await Shell.Current.GoToAsync(nameof(App_enter));
        }
    }

}
