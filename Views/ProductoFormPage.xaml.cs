using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;

namespace LetyAccesoriosApp.Views
{
    public partial class ProductoFormPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ProductoVenta _productoActual;

        // El constructor ahora recibe el DatabaseService correcto
        public ProductoFormPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _productoActual = new ProductoVenta();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Lógica para cargar insumos disponibles al abrir el formulario
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Ejemplo de guardado integrado con el servicio nuevo
            if (string.IsNullOrWhiteSpace(_productoActual.Nombre)) return;

            var recetaVacia = new List<ProductoInsumo>();
            await _databaseService.GuardarProductoConRecetaAsync(_productoActual, recetaVacia);
            await Navigation.PopAsync();
        }
    }
}
