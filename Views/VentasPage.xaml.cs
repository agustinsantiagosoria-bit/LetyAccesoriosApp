using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;

namespace LetyAccesoriosApp.Views
{
    public partial class VentasPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public VentasPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var historial = await _databaseService.ObtenerHistorialVentasAsync();
        }

        private void OnFiltroHoyClicked(object sender, EventArgs e)
        {
            // Lógica diaria
        }

        private void OnFiltroSemanaClicked(object sender, EventArgs e)
        {
            // Lógica semanal
        }

        // SE AGREGÓ ESTA FUNCIÓN PARA RESOLVER EL ERROR DEL BOTÓN MENSUAL
        private void OnFiltroMesClicked(object sender, EventArgs e)
        {
            // Lógica mensual
        }

        private async void OnCancelarVentaClicked(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.CommandParameter is int ventaId)
            {
                await _databaseService.CancelarVentaAsync(ventaId);
                OnAppearing(); 
            }
        }
    }
}
