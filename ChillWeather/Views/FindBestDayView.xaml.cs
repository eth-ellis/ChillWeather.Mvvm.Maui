using ChillWeather.ViewModels;

namespace ChillWeather.Views;

public partial class FindBestDayView : ContentPage
{
    public FindBestDayView(FindBestDayViewModel viewModel)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel;
    }
}