using Microsoft.Extensions.Logging;
using MyGarage.Pages;
using MyGarage.Repositories;
using MyGarage.Repositories.Interfaces;
using MyGarage.Services;
using MyGarage.ViewModels;

namespace MyGarage
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();
            builder.Services.AddTransient<IServiceRecordRepository, ServiceRecordRepository>();

            builder.Services.AddTransient<VehicleListViewModel>();
            builder.Services.AddTransient<VehicleEntryViewModel>();
            builder.Services.AddTransient<ServiceLogViewModel>();
            builder.Services.AddTransient<ServiceEntryViewModel>();

            builder.Services.AddTransient<VehicleListPage>();
            builder.Services.AddTransient<VehicleEntryPage>();
            builder.Services.AddTransient<ServiceLogPage>();
            builder.Services.AddTransient<ServiceEntryPage>();

            builder.Services.AddSingleton<AppShell>();  

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
