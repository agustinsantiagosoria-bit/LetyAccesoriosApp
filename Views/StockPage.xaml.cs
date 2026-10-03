using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.ViewModels;

namespace LetyAccesoriosApp.Views;

public partial class StockPage : ContentPage
{
    private readonly StockViewModel _viewModel;
    private readonly DatabaseService _databaseService;

    // Recibe el servicio por el constructor de forma automática gracias a MAUI
    public StockPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
        
        // Le pasamos el servicio requerido al ViewModel
        _viewModel = new StockViewModel(_databaseService);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarProductosAsync();
    }

    private async void OnAgregarProductoClicked(object sender, EventArgs e)
    {
        // Le pasamos el servicio a la página del formulario correctamente
        await Navigation.PushAsync(new ProductoFormPage(_databaseService));
    }
}
