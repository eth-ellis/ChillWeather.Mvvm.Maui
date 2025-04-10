using ChillWeather.ViewModels;

namespace ChillWeather.Views;

public partial class PickLocationView : ContentPage
{
    public PickLocationView(PickLocationViewModel viewModel)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel;
    }
}