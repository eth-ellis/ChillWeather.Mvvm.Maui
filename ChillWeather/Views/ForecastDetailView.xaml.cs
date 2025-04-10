using ChillWeather.ViewModels;

namespace ChillWeather.Views;

public partial class ForecastDetailView : ContentPage
{
    public ForecastDetailView(ForecastDetailViewModel viewModel)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel;
    }
}