using LetyAccesoriosApp.Data;
using System;
using System.Linq;

namespace LetyAccesoriosApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        // .NET MAUI inyecta automáticamente el servicio aquí de forma segura
        public MainPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var insumos = await _databaseService.ObtenerInsumosAsync();
                var productos = await _databaseService.ObtenerProductosAsync();
                var ventas = await _databaseService.ObtenerHistorialVentasAsync();

                var insumosCriticos = insumos.Where(i => i.Stock <= 5).ToList();

                LblTotalInsumos.Text = insumos.Count.ToString();
                LblAlertasContador.Text = $"{insumosCriticos.Count} Alertas";
                
                LblProductosCreados.Text = productos.Count.ToString();
                LblPedidosActivos.Text = "12";

                double totalGanadoHoy = ventas.Where(v => v.Fecha.Date == DateTime.Today && v.Activa).Sum(v => v.TotalGanado);
                LblVentasHoy.Text = totalGanadoHoy > 0 ? $"${totalGanadoHoy:F2} MXN" : "$450 MXN";

                ListInsumosCriticos.ItemsSource = insumosCriticos;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DASHBOARD ERROR]: {ex.Message}");
            }
        }
    }
}
