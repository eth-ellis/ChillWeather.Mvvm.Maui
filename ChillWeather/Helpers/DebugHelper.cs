using System.Diagnostics;

namespace ChillWeather.Helpers;

public static class DebugHelper
{
    public static void WriteScenario(string scenario)
    {
        Debug.WriteLine("========== Scenario ==========");
        Debug.WriteLine(scenario);
        Debug.WriteLine("");
    }

    public static void WriteQueryAttributes(IDictionary<string, object> query, string viewModelName)
    {
        if (query.Count == 0)
        {
            return;
        }
        
        Debug.WriteLine("---------- Query Attributes ----------");
        Debug.WriteLine($"ViewModel: {viewModelName}");
        Debug.WriteLine("Parameters:");
        Debug.WriteLine(string.Join(Environment.NewLine, query));
        Debug.WriteLine("");
    }

    public static void WriteInitialisation(string viewModelName)
    {
        Debug.WriteLine("---------- InitialiseAsync ----------");
        Debug.WriteLine($"ViewModel: {viewModelName}");
        Debug.WriteLine("");
    }

    public static void WriteReinitialisation(string viewModelName)
    {
        Debug.WriteLine("---------- ReinitialiseAsync ----------");
        Debug.WriteLine($"ViewModel: {viewModelName}");
        Debug.WriteLine("");
    }
    
    public static void WriteNote(params List<string> lines)
    {
        Debug.WriteLine("> Note");
        
        foreach (var line in lines)
        {
            Debug.WriteLine($"> {line}");
        }
        
        Debug.WriteLine("");
    }
}