using LetyAccesoriosApp.ViewModels;
using LetyAccesoriosApp.Data;

namespace LetyAccesoriosApp.Views;

public partial class InsumosPage : ContentPage
{
    private readonly InsumosViewModel _viewModel;

    public InsumosPage(DatabaseService databaseService)
    {
        InitializeComponent();
        
        _viewModel = new InsumosViewModel(databaseService);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarDatosAsync();
    }
}
