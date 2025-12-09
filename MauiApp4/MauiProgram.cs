using MauiApp4.Services;
using MauiApp4.ViewModel;
using Microsoft.Extensions.Logging;

namespace MauiApp4
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
                    fonts.AddFont("Vokiar.otf", "Vokiar");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            RegisterPagesAndVM(builder.Services);
            return builder.Build();
        }

        private static void RegisterPagesAndVM(IServiceCollection service)
        {
            service.AddSingleton<HttpClient>();
            service.AddTransient<ContactsPage>();
            service.AddSingleton<IApiService, ApiService>();
            service.AddTransient<ContactsViewModel>();
        }
    }
}
