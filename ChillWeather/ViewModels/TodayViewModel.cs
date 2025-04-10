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
        await this.navigationService.GoToRootAsync<PickLocationView>(new Dictionary<string, object>
        {
            { "Route", nameof(TodayView) }
        });
    }
    
    [RelayCommand]
    private async Task OpenWarning(Warning warning)
    {
        await this.navigationService.GoToAsync<WarningView>(new Dictionary<string, object>
        {
            { "Warning", warning }
        });
    }
}