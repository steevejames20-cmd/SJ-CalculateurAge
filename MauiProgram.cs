using CalculateurAge.ViewModels;
using CalculateurAge.Views;

namespace CalculateurAge;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Un seul ViewModel partage par les deux pages.
        builder.Services.AddSingleton<CalculateurViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ResultatPage>();

        return builder.Build();
    }
}
