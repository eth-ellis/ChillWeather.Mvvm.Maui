using ChillWeather.Helpers;
using ChillWeather.Models;
using ChillWeather.Services;
using ChillWeather.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChillWeather.ViewModels;

public partial class WarningViewModel : BaseViewModel, IQueryAttributable
{
    private readonly INavigationService navigationService;
    
    public WarningViewModel(
        INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }
    
    [ObservableProperty]
    private Warning warning;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        DebugHelper.WriteQueryAttributes(query, nameof(WarningViewModel));

        if (query.TryGetValue("Warning", out var warning) && warning is Warning warningValue)
        {
            this.Warning = warningValue;
        }
    }
    
    [RelayCommand]
    private async Task Close()
    {
        await this.navigationService.GoBackAsync();
    }
}