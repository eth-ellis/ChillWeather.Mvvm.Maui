using ChillWeather.ViewModels;

namespace ChillWeather.Views;

public partial class ForecastView : ContentPage
{
    public ForecastView(ForecastViewModel viewModel)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel;
    }
}