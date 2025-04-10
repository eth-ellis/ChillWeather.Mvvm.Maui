namespace ChillWeather.Services;

public class PreferenceService : IPreferenceService
{
    public string Location
    {
        get => Preferences.Get("Location", "Adelaide, South Australia");
        set => Preferences.Set("Location", value);
    }
}