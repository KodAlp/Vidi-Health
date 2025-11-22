
using Vidi_Health.Services.Database_Services;

namespace Vidi_Health
{
    public partial class App : Application
    {
        public static IDataBaseService DatabaseService { get; private set; }

        public App(IDataBaseService databaseService)
        {
            InitializeComponent();

            DatabaseService = databaseService;

            // Database initialization
            InitializeDatabaseAsync();

            MainPage = new AppShell();
        }
        private static async void InitializeDatabaseAsync()
        {
            try
            {
                await DatabaseService.InitializeDatabaseAsync();
            }
            catch (Exception ex)
            {
                // Log error - production'da logging service kullan
                System.Diagnostics.Debug.WriteLine($"DB Init Error: {ex.Message}");
            }
        }
    }
}
