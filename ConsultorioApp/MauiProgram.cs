using Microsoft.Extensions.Logging;

namespace ConsultorioApp {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Database
            builder.Services.AddSingleton<Data.DatabaseContext>();

            // Services
            builder.Services.AddSingleton<Services.PacienteService>();
            builder.Services.AddSingleton<Services.ConsultaService>();

            // ViewModels
            builder.Services.AddTransient<ViewModels.PacientesViewModel>();
            builder.Services.AddTransient<ViewModels.PacienteFormViewModel>();
            builder.Services.AddTransient<ViewModels.AgendaViewModel>();
            builder.Services.AddTransient<ViewModels.ConsultaFormViewModel>();

            // Pages
            builder.Services.AddTransient<Views.PacientesPage>();
            builder.Services.AddTransient<Views.PacienteFormPage>();
            builder.Services.AddTransient<Views.AgendaPage>();
            builder.Services.AddTransient<Views.ConsultaFormPage>();

            return builder.Build();
        }
    }
}
