using ChillWeather.Services;
using ChillWeather.ViewModels.Base;
using ChillWeather.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChillWeather.ViewModels;

public partial class PickLocationViewModel : BaseViewModel, IQueryAttributable
{
    private readonly INavigationService navigationService;
    private readonly IPreferenceService preferenceService;
    private readonly IDataService dataService;

    private string route;
    
    public PickLocationViewModel(
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
    private List<string> locations;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Route", out var route) && route is string routeValue)
        {
            this.route = routeValue;
        }
    }
    
    public override async Task InitialiseAsync()
    {
        await base.InitialiseAsync();
        
        this.Locations = await dataService.GetLocationsAsync();

        this.IsBusy = false;
    }
    
    [RelayCommand]
    private async Task PickLocation(string location)
    {
        this.preferenceService.Location = location;

        if (this.route == nameof(TodayView))
        {
            await this.navigationService.GoToRootAsync<TodayView>();
        }
        
        if (this.route == nameof(ForecastView))
        {
            await this.navigationService.GoToRootAsync<ForecastView>();
        }
    }
    
    [RelayCommand]
    private async Task Cancel()
    {
        if (this.route == nameof(TodayView))
        {
            await this.navigationService.GoToRootAsync<TodayView>();
        }
        
        if (this.route == nameof(ForecastView))
        {
            await this.navigationService.GoToRootAsync<ForecastView>();
        }
    }
}