using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;
using System.Linq;

namespace LetyAccesoriosApp.Views
{
    public partial class VentasPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        // El constructor recibe el inyector de dependencias de la base de datos
        public VentasPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }
        // Recarga el historial de ventas automáticamente al entrar a la pantalla
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // 1. Consultamos el historial de tickets registrados en SQLite
                var listaVentas = await _databaseService.ObtenerHistorialVentasAsync();

                // 2. Vinculamos el resultado directamente con el listado XAML mediante un objeto anónimo
                BindingContext = new { HistorialVentas = listaVentas };
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[VENTAS ERROR]: {ex.Message}");
            }
        }
    }
}
