using ChillWeather.Models;

namespace ChillWeather.Services;

public interface IDataService
{
    Task<List<string>> GetLocationsAsync();
    
    Task<Forecast> GetForecastForTodayAsync();
    
    Task<List<Forecast>> GetForecastForWeekAsync();

    Task<List<Warning>> GetWarningsAsync();
}