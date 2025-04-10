namespace ChillWeather.Services;

public class NavigationService : INavigationService
{
    public async Task GoToAsync<TView>(Dictionary<string, object>? parameters = null)
        where TView : ContentPage
    {
        parameters ??= [];
        
        await Shell.Current.GoToAsync(typeof(TView).Name, new ShellNavigationQueryParameters(parameters));
    }
    
    public async Task GoToRootAsync<TView>(Dictionary<string, object>? parameters = null)
        where TView : ContentPage
    {
        parameters ??= [];
        
        await Shell.Current.GoToAsync($"//{typeof(TView).Name}", new ShellNavigationQueryParameters(parameters));
    }

    public async Task GoBackAsync(Dictionary<string, object>? parameters = null)
    {
        parameters ??= [];

        await Shell.Current.GoToAsync("..", new ShellNavigationQueryParameters(parameters));
    }
}