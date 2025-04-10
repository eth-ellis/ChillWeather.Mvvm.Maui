using ChillWeather.ViewModels;

namespace ChillWeather.Views;

public partial class WarningView : ContentPage
{
    public WarningView(WarningViewModel viewModel)
    {
        InitializeComponent();
        
        this.BindingContext = viewModel;
    }
}