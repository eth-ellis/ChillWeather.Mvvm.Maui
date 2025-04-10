namespace ChillWeather.Services;

public interface INavigationService
{
    Task GoToAsync<TView>(Dictionary<string, object>? parameters = null)
        where TView : ContentPage;
    
    Task GoToRootAsync<TView>(Dictionary<string, object>? parameters = null)
        where TView : ContentPage;
    
    Task GoBackAsync(Dictionary<string, object>? parameters = null);
}