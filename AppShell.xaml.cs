using Vidi_Health.Pages;

namespace Vidi_Health
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(App_enter), typeof(App_enter));
            Routing.RegisterRoute(nameof(Hall_Page), typeof(Hall_Page));

        }
    }
}
