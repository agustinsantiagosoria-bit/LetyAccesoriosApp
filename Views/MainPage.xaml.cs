using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;
using System.Linq;

namespace LetyAccesoriosApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        // El constructor recibe el servicio único de base de datos local
        public MainPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }
        // Este método se ejecuta automáticamente cada vez que se abre la pantalla
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // 1. Consultamos los registros guardados en SQLite
                var insumos = await _databaseService.ObtenerInsumosAsync();
                var productos = await _databaseService.ObtenerProductsAsync();
                var ventas = await _databaseService.ObtenerHistorialVentasAsync();

                // 2. Filtramos los insumos críticos con bajo stock (menor o igual a 5 unidades)
                var insumosCriticos = insumos.Where(i => i.Stock <= 5).ToList();

                // 3. Pintamos los números calculados en las etiquetas del diseño XAML
                LblTotalInsumos.Text = insumos.Count.ToString();
                LblAlertasContador.Text = $"{insumosCriticos.Count} Alertas";
                
                LblProductosCreados.Text = productos.Count.ToString();
                LblPedidosActivos.Text = "12"; // Valor de maqueta estático para pedidos activos

                // Calculamos el total de dinero ganado en las ventas del día de hoy
                double totalGanadoHoy = ventas.Where(v => v.Fecha.Date == System.DateTime.Today && v.Activa).Sum(v => v.TotalGanado);
                LblVentasHoy.Text = totalGanadoHoy > 0 ? $"${totalGanadoHoy:F2} MXN" : "$450 MXN"; // Fallback estático de tu maqueta

                // 4. Enlazamos la lista de alertas del panel derecho de forma asíncrona
                ListInsumosCriticos.ItemsSource = insumosCriticos;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DASHBOARD ERROR]: {ex.Message}");
            }
        }
    }
}
