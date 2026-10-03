using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;
using System.Linq;

namespace LetyAccesoriosApp.Views
{
    public partial class ProductoFormPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        // El constructor recibe el servicio único de base de datos local mediante inyección
        public ProductoFormPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }
        // Recarga el catálogo de productos automáticamente al entrar a la pantalla
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // 1. Consultamos la lista de accesorios guardados en SQLite
                var listaProductos = await _databaseService.ObtenerProductsAsync();

                // 2. Vinculamos el resultado directamente con la grilla XAML mediante un objeto anónimo
                BindingContext = new { Productos = listaProductos };
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CATALOGO ERROR]: {ex.Message}");
            }
        }
    }
}
