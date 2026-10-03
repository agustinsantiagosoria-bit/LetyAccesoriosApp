using LetyAccesoriosApp.Data;
using System;

namespace LetyAccesoriosApp.Views
{
    public partial class ProductoFormPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public ProductoFormPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var listaProductos = await _databaseService.ObtenerProductosAsync();
                BindingContext = new { Productos = listaProductos };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CATALOGO ERROR]: {ex.Message}");
            }
        }
    }
}
