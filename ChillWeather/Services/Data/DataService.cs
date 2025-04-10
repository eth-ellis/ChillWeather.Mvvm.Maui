using ChillWeather.Models;

namespace ChillWeather.Services;

public class DataService : IDataService
{
    private readonly Random random = new();
    
    private readonly string[] directions =
    [
        "N", "NNE", "NE", "ENE",
        "E", "ESE", "SE", "SSE",
        "S", "SSW", "SW", "WSW",
        "W", "WNW", "NW", "NNW"
    ];
    
    private readonly string[] warnings =
    [
        "Heatwave",
        "Flood",
        "Wind",
        "Marine wind",
        "Bushfire",
        "High tide",
        "Sheep grazier",
        "Thunderstorm"
    ];
    
    private readonly string[] impactedAreas =
    [
        "Willowvale",
        "Greenwood",
        "Riverside",
        "Meadowbrook",
        "Hillview",
        "Sunset Hills",
        "Oakwood",
        "Brookfield",
        "Riverview",
        "Pinecrest",
        "Springvale",
        "Glenwood",
        "Woodlands",
        "Fairview",
        "Parkside"
    ];
    
    public async Task<List<string>> GetLocationsAsync()
    {
        await Task.Delay(500);

        return
        [
            "Adelaide, South Australia",
            "Brisbane, Queensland",
            "Sydney, New South Wales",
            "Melbourne, Victoria",
            "Perth, Western Australia",
            "Darwin, Northern Territory",
            "Canberra, Australian Capital Territory",
            "Hobart, Tasmania",
            "Maui, Hawaii"
        ];
    }

    public async Task<Forecast> GetForecastForTodayAsync()
    {
        await Task.Delay(500);

        return this.GenerateForecast(includeCurrentTemperature: true);
    }

    public async Task<List<Forecast>> GetForecastForWeekAsync()
    {
        await Task.Delay(500);

        return
        [
            this.GenerateForecast(dayOfWeek: DayOfWeek.Sunday),
            this.GenerateForecast(dayOfWeek: DayOfWeek.Monday),
            this.GenerateForecast(dayOfWeek: DayOfWeek.Tuesday),
            this.GenerateForecast(dayOfWeek: DayOfWeek.Wednesday),
            this.GenerateForecast(dayOfWeek: DayOfWeek.Thursday),
            this.GenerateForecast(dayOfWeek: DayOfWeek.Friday),
            this.GenerateForecast(dayOfWeek: DayOfWeek.Saturday)
        ];
    }
    
    public async Task<List<Warning>> GetWarningsAsync()
    {
        await Task.Delay(500);

        return
        [
            this.GenerateWarning()
        ];
    }

    private Forecast GenerateForecast(bool includeCurrentTemperature = false, DayOfWeek? dayOfWeek = null)
    {
        var forecast = new Forecast
        {
            MaxTemperature = Math.Round(this.random.Next(15, 35) + this.random.NextDouble(), 1),
            MinTemperature = Math.Round(this.random.Next(5, 15) + this.random.NextDouble(), 1),
            HumidityPercentage = this.random.Next(0, 101),
            ChanceOfRainPercentage = this.random.Next(0, 101),
            MillimetersOfRain = this.random.Next(0, 15),
            SunriseTime = new TimeOnly(this.random.Next(6, 8), this.random.Next(0, 60)),
            SunsetTime = new TimeOnly(this.random.Next(18, 20), this.random.Next(0, 60)),
            WindDirection = this.directions[this.random.Next(this.directions.Length)],
            WindSpeed = this.random.Next(0, 60)
        };

        if (dayOfWeek is not null)
        {
            forecast.DayOfWeek = dayOfWeek.Value;
        }

        if (includeCurrentTemperature)
        {
            forecast.CurrentTemperature = Math.Round(this.random.Next((int)forecast.MinTemperature, (int)forecast.MaxTemperature) + this.random.NextDouble(), 1);
        }
        
        return forecast;
    }
    
    private Warning GenerateWarning()
    {
        var title = $"{this.warnings[this.random.Next(this.warnings.Length)]} warning";

        this.random.Shuffle(this.impactedAreas);
        
        var randomAreas = this.impactedAreas.Take(5);
        
        return new Warning
        {
            Title = title,
            Description = $"The areas impacted by this {title.ToLower()} are: {string.Join(", ", randomAreas)}."
        };
    }
}