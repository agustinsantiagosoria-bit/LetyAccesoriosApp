using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;

namespace LetyAccesoriosApp.Views
{
    public partial class VentasPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        // Constructor corregido con el tipo DatabaseService
        public VentasPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Actualiza la lista de ventas cargadas en la interfaz de usuario
            var historial = await _databaseService.ObtenerHistorialVentasAsync();
            // HistorialCollectionView.ItemsSource = historial; // Descomenta si usas este control en tu XAML
        }

        private async void OnCancelarVentaClicked(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.CommandParameter is int ventaId)
            {
                await _databaseService.CancelarVentaAsync(ventaId);
                OnAppearing(); // Refresca la UI al anular
            }
        }
    }
}
