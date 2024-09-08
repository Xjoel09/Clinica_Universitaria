using Microsoft.Extensions.Logging;
using MedicalUTP.DataAcess;
using Microsoft.EntityFrameworkCore;
using MedicalUTP.Pages;
//using MedicalUTP.ViewsModel;

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
            builder.Services.AddTransient<Register>();
            builder.Services.AddTransient<Login>();

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
