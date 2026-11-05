using MyGarage.Pages;
using MyGarage.Services;

namespace MyGarage
{
    public partial class AppShell : Shell
    {
        private readonly DatabaseService _databaseService;
        private bool _isInitialized;

        public AppShell(DatabaseService databaseService)
        {
            InitializeComponent();

            _databaseService = databaseService;

            Routing.RegisterRoute(nameof(VehicleEntryPage), typeof(VehicleEntryPage));
            Routing.RegisterRoute(nameof(ServiceLogPage), typeof(ServiceLogPage));
            Routing.RegisterRoute(nameof(ServiceEntryPage), typeof(ServiceEntryPage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_isInitialized)
                return;

            _isInitialized = true;

            try
            {
                await _databaseService.InitializeAsync();
            }
            catch (Exception ex)
            {
                _isInitialized = false;
                await DisplayAlertAsync("Database error", ex.Message, "OK");
            }
        }
    }
}