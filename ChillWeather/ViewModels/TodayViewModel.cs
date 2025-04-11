using ChillWeather.Helpers;
using ChillWeather.Models;
using ChillWeather.Services;
using ChillWeather.ViewModels.Base;
using ChillWeather.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChillWeather.ViewModels;

public partial class TodayViewModel : BaseViewModel
{
    private readonly INavigationService navigationService;
    private readonly IPreferenceService preferenceService;
    private readonly IDataService dataService;
    
    public TodayViewModel(
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
    private Forecast forecast;
    
    [ObservableProperty]
    private List<Warning> warnings;
    
    public override async Task InitialiseAsync()
    {
        await base.InitialiseAsync();

        var forecastTask = this.dataService.GetForecastForTodayAsync();
        var warningsTask = this.dataService.GetWarningsAsync();
        
        await Task.WhenAll(forecastTask, warningsTask);
        
        this.Forecast = forecastTask.Result;
        this.Warnings = warningsTask.Result;
        
        this.IsBusy = false;
    }
    
    [RelayCommand]
    private async Task Refresh()
    {
        var forecastTask = this.dataService.GetForecastForTodayAsync();
        var warningsTask = this.dataService.GetWarningsAsync();
        
        await Task.WhenAll(forecastTask, warningsTask);
        
        this.Forecast = forecastTask.Result;
        this.Warnings = warningsTask.Result;

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
            { "Route", nameof(TodayView) }
        });
    }
    
    [RelayCommand]
    private async Task OpenWarning(Warning warning)
    {
        DebugHelper.WriteScenario("Pass data to ViewModel during modal navigation");

        await this.navigationService.GoToAsync<WarningView>(new Dictionary<string, object>
        {
            { "Warning", warning }
        });
    }
}