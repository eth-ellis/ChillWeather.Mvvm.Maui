using ChillWeather.ViewModels;

namespace ChillWeather.Views;

public partial class TodayView : ContentPage
{
    public TodayView(TodayViewModel viewModel)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel;
    }
}