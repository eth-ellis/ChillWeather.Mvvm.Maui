using ChillWeather.Services;
using ChillWeather.ViewModels;
using ChillWeather.Views;
using CommunityToolkit.Maui;

namespace ChillWeather;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterNavigation()
            .RegisterServices();

        return builder.Build();
    }
    
    private static MauiAppBuilder RegisterNavigation(this MauiAppBuilder builder)
    {
        builder.Services.AddTransientWithShellRoute<FindBestDayView, FindBestDayViewModel>(nameof(FindBestDayView));
        builder.Services.AddTransientWithShellRoute<ForecastDetailView, ForecastDetailViewModel>(nameof(ForecastDetailView));
        builder.Services.AddTransientWithShellRoute<ForecastView, ForecastViewModel>(nameof(ForecastView));
        builder.Services.AddTransientWithShellRoute<PickLocationView, PickLocationViewModel>(nameof(PickLocationView));
        builder.Services.AddTransientWithShellRoute<TodayView, TodayViewModel>(nameof(TodayView));
        builder.Services.AddTransientWithShellRoute<WarningView, WarningViewModel>(nameof(WarningView));
		
        return builder;
    }
	
    private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IDataService, DataService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPreferenceService, PreferenceService>();
        
        return builder;
    }
}