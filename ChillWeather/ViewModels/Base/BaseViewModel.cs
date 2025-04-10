using CommunityToolkit.Mvvm.ComponentModel;

namespace ChillWeather.ViewModels.Base;

public partial class BaseViewModel : ObservableObject
{
    internal bool HasInitialised { get; set; } = false;
    
    [ObservableProperty]
    private bool isBusy = true;
    
    [ObservableProperty]
    private bool isRefreshing = false;
    
    public virtual Task InitialiseAsync()
    {
        return Task.CompletedTask;
    }
    
    public virtual Task ReinitialiseAsync()
    {
        return Task.CompletedTask;
    }
}