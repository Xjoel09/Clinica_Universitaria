using Microsoft.Extensions.Logging;
using MedicalUTP.DataAcess;
using Microsoft.EntityFrameworkCore;
using MedicalUTP.Pages;
using MedicalUTP.ViewsModel;
using MedicalUTP.ViewModel;
using MedicalUTP.Utilidades;

namespace MedicalUTP
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
                    fonts.AddFont("Menu-Icon.ttf", "Menu");
                });

            builder.Services.AddDbContext<MedicalUTPDbContext>();
            
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<Register>();
            builder.Services.AddTransient<CRUDAdmin>();
            builder.Services.AddTransient<ConsultaViewModel>();
            builder.Services.AddTransient<HistorialCitasViewModel>();
            builder.Services.AddTransient<HistorialCitas>();
            builder.Services.AddTransient<Solicitud>();

            var dbContext = new MedicalUTPDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();
            


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
