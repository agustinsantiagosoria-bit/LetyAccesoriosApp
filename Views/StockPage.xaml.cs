using LetyAccesoriosApp.ViewModels;

namespace LetyAccesoriosApp.Views;

public partial class StockPage : ContentPage
{
    private readonly StockViewModel _viewModel;

    public StockPage()
    {
        InitializeComponent();
        _viewModel = new StockViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarProductosAsync();
    }

    private async void OnAgregarProductoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductoFormPage());
    }
}