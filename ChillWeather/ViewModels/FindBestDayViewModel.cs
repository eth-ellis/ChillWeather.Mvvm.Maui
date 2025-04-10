using ChillWeather.Services;
using ChillWeather.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;

namespace ChillWeather.ViewModels;

public partial class FindBestDayViewModel : BaseViewModel
{
    private readonly INavigationService navigationService;
    
    public FindBestDayViewModel(
        INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }
    
    [RelayCommand]
    private async Task HotWeather()
    {
        await this.navigationService.GoBackAsync(new Dictionary<string, object>
        {
            { "WeatherPreference", "Hot" }
        });
    }
    
    [RelayCommand]
    private async Task ColdWeather()
    {
        await this.navigationService.GoBackAsync(new Dictionary<string, object>
        {
            { "WeatherPreference", "Cold" }
        });
    }
    
    [RelayCommand]
    private async Task Cancel()
    {
        await this.navigationService.GoBackAsync();
    }
}