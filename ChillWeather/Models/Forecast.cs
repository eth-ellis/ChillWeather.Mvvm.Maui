using CommunityToolkit.Mvvm.ComponentModel;

namespace ChillWeather.Models;

public partial class Forecast : ObservableObject
{
    public DayOfWeek DayOfWeek { get; set; }
    
    public double CurrentTemperature { get; set; }
    
    public required double MaxTemperature { get; init; }
    
    public required double MinTemperature { get; init; }
    
    public required int HumidityPercentage { get; init; }
    
    public required int ChanceOfRainPercentage { get; init; }
    
    public required int MillimetersOfRain { get; init; }
    
    public required TimeOnly SunriseTime { get; init; }
    
    public required TimeOnly SunsetTime { get; init; }
    
    public required string WindDirection { get; init; }
    
    public required double WindSpeed { get; init; }

    [ObservableProperty]
    private bool isBestDay;
}