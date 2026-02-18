using Vidi_Health.Pages;
namespace Vidi_Health
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoogleLoginClicked(object sender, EventArgs e)
        {
            // Google OAuth buraya gelecek
        }

        private async void OnAppleLoginClicked(object sender, EventArgs e)
        {
            // Apple Sign-In buraya gelecek
        }

        private async void OnManualLoginTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(App_enter));
        }
    }
}
