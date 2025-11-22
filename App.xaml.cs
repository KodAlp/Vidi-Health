
using Vidi_Health.Services.Database_Services;

namespace Vidi_Health
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Database initialization
            MainPage = new AppShell();
        }
    }
}
