using ChillWeather.Helpers;
using ChillWeather.Models;
using ChillWeather.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChillWeather.ViewModels;

public partial class ForecastDetailViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    private Forecast forecast;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        DebugHelper.WriteQueryAttributes(query, nameof(ForecastDetailViewModel));

        if (query.TryGetValue("Forecast", out var forecast) && forecast is Forecast forecastValue)
        {
            this.Forecast = forecastValue;
        }
    }
}