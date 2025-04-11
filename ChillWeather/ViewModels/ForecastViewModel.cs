using ChillWeather.Helpers;
using ChillWeather.Models;
using ChillWeather.Services;
using ChillWeather.ViewModels.Base;
using ChillWeather.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChillWeather.ViewModels;

public partial class ForecastViewModel : BaseViewModel, IQueryAttributable
{
    private readonly INavigationService navigationService;
    private readonly IPreferenceService preferenceService;
    private readonly IDataService dataService;
    
    private string? weatherPreference;
    
    public ForecastViewModel(
        INavigationService navigationService,
        IPreferenceService preferenceService,
        IDataService dataService)
    {
        this.navigationService = navigationService;
        this.preferenceService = preferenceService;
        this.dataService = dataService;
    }
    
    public string CurrentLocation
        => this.preferenceService.Location;
    
    [ObservableProperty]
    private List<Forecast> forecast;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        DebugHelper.WriteQueryAttributes(query, nameof(ForecastViewModel));
        
        if (query.TryGetValue("WeatherPreference", out var weatherPreference) && weatherPreference is string weatherPreferenceValue)
        {
            this.weatherPreference = weatherPreferenceValue;
        }
    }
    
    public override async Task InitialiseAsync()
    {
        await base.InitialiseAsync();

        this.Forecast = await this.dataService.GetForecastForWeekAsync();
        
        this.IsBusy = false;
    }

    public override async Task ReinitialiseAsync()
    {
        await base.ReinitialiseAsync();

        if (this.weatherPreference is null)
        {
            return;
        }
        
        var forecastOrderedByTemperature = this.Forecast
            .OrderBy(forecast => forecast.MaxTemperature)
            .ToList();

        foreach (var forecast in forecastOrderedByTemperature)
        {
            forecast.IsBestDay = false;
        }
        
        if (this.weatherPreference == "Hot")
        {
            forecastOrderedByTemperature.Last().IsBestDay = true;
        }
        
        if (this.weatherPreference == "Cold")
        {
            forecastOrderedByTemperature.First().IsBestDay = true;
        }

        this.weatherPreference = null;
    }

    [RelayCommand]
    private async Task Refresh()
    {
        this.Forecast = await this.dataService.GetForecastForWeekAsync();

        this.IsRefreshing = false;
    }
    
    [RelayCommand]
    private async Task ChangeLocation()
    {
        DebugHelper.WriteScenario("Pass data to ViewModel during root navigation");
        DebugHelper.WriteNote(
            "When navigating to root i.e. changing shell item, ApplyQueryAttributes fires after InitialiseAsync.",
            "This is caused by https://github.com/dotnet/maui/issues/24241",
            "This can be seen below:");
        
        await this.navigationService.GoToRootAsync<PickLocationView>(new Dictionary<string, object>
        {
            { "Route", nameof(ForecastView) }
        });
    }
    
    [RelayCommand]
    private async Task FindBestDay()
    {
        DebugHelper.WriteScenario("Open modal which will pass data to previous ViewModel on backwards navigation");
        
        await this.navigationService.GoToAsync<FindBestDayView>();
    }
    
    [RelayCommand]
    private async Task OpenDetail(Forecast forecast)
    {
        DebugHelper.WriteScenario("Pass data to ViewModel during normal navigation");
        
        await this.navigationService.GoToAsync<ForecastDetailView>(new Dictionary<string, object>
        {
            { "Forecast", forecast }
        });
    }
}