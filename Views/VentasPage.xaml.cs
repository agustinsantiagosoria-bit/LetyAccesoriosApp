using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetyAccesoriosApp.Views
{
    public partial class VentasPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly List<DetalleVenta> _carritoActual = new();

        public VentasPage()
        {
            InitializeComponent();
            _databaseService = IPlatformApplication.Current!.Services.GetRequiredService<DatabaseService>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarDatosPantallaAsync();
        }

        private async Task CargarDatosPantallaAsync()
        {
            try
            {
                var listaVentas = await _databaseService.ObtenerHistorialVentasAsync();
                var listaProductos = await _databaseService.ObtenerProductosAsync();

                PickerProductosVenta.ItemsSource = listaProductos;
                PickerProductosVenta.ItemDisplayBinding = new Binding("Nombre");
                
                BindingContext = new { HistorialVentas = listaVentas };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BOX ERROR]: {ex.Message}");
            }
        }

        private async void OnRegistrarVentaClicked(object sender, EventArgs e)
        {
            if (PickerProductosVenta.SelectedItem is ProductoVenta productoSelected && 
                double.TryParse(TxtCantidadVenta.Text, out double cantidadUnidades))
            {
                try
                {
                    _carritoActual.Clear();
                    _carritoActual.Add(new DetalleVenta
                    {
                        ProductoVentaId = productoSelected.Id,
                        Cantidad = (int)cantidadUnidades,
                        PrecioUnitarioVenta = productoSelected.PrecioVenta
                    });

                    await _databaseService.RegistrarVentaAsync(_carritoActual);
                    
                    TxtCantidadVenta.Text = string.Empty;
                    PickerProductosVenta.SelectedItem = null;
                    
                    await DisplayAlert("🌸 Caja Actualizada", $"Se registró la venta de {productoSelected.Nombre}.", "Entendido");
                    await CargarDatosPantallaAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo completar la transacción: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Atención", "Por favor selecciona un accesorio del catálogo e ingresa una cantidad válida.", "OK");
            }
        }
    }
}
