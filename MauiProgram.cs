using Microsoft.Extensions.Logging;

using Week6_RESTFULAPI.Services;
using Week6_RESTFULAPI.ViewModels;
using Week6_RESTFULAPI.Views;

namespace Week6_RESTFULAPI
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Register Services
            // STEP 1: Use GitHub Service
            builder.Services.AddSingleton<IStudentService, GitHubStudentService>();

            // STEP 2: Later, comment line above and uncomment below to use Local API
            // builder.Services.AddSingleton<IStudentService, LocalApiStudentService>();

            // Register ViewModels
            builder.Services.AddSingleton<StudentsViewModel>();
            builder.Services.AddTransient<AddStudentViewModel>();

            // Register Views
            builder.Services.AddSingleton<StudentPage>();
            builder.Services.AddTransient<AddStudentPage>();

            return builder.Build();
        }
    }
}
