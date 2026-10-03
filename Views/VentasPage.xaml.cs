using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models;

namespace LetyAccesoriosApp.Views;

public partial class VentasPage : ContentPage
{
    private readonly DatabaseContext _database;

    public VentasPage()
    {
        InitializeComponent();
        _database = new DatabaseContext();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarVentasHoyAsync();
    }

    private async Task CargarVentasPorRangoAsync(DateTime inicio, DateTime fin)
    {
        var ventas = await _database.GetVentasByFechaAsync(inicio, fin);
        VentasCollectionView.ItemsSource = ventas;

        decimal total = ventas.Sum(v => v.TotalVenta);
        TotalRecaudadoLabel.Text = $"${total:N2}";
    }

    private async void OnFiltroHoyClicked(object sender, EventArgs e)
    {
        ActualizarEstiloBotones(BtnHoy, BtnSemana, BtnMes);
        await CargarVentasHoyAsync();
    }

    private async Task CargarVentasHoyAsync()
    {
        var hoyInicio = DateTime.Today;
        var hoyFin = DateTime.Today.AddDays(1).AddTicks(-1);
        await CargarVentasPorRangoAsync(hoyInicio, hoyFin);
    }

    private async void OnFiltroSemanaClicked(object sender, EventArgs e)
    {
        ActualizarEstiloBotones(BtnSemana, BtnHoy, BtnMes);
        var inicioSemana = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        var finSemana = DateTime.Today.AddDays(1).AddTicks(-1);
        await CargarVentasPorRangoAsync(inicioSemana, finSemana);
    }

    private async void OnFiltroMesClicked(object sender, EventArgs e)
    {
        ActualizarEstiloBotones(BtnMes, BtnHoy, BtnSemana);
        var inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var finMes = DateTime.Today.AddDays(1).AddTicks(-1);
        await CargarVentasPorRangoAsync(inicioMes, finMes);
    }

    private void ActualizarEstiloBotones(Button activo, Button inactivo1, Button inactivo2)
    {
        activo.BackgroundColor = Color.FromArgb("#2196F3");
        activo.TextColor = Colors.White;

        inactivo1.BackgroundColor = Color.FromArgb("#E0E0E0");
        inactivo1.TextColor = Colors.Black;

        inactivo2.BackgroundColor = Color.FromArgb("#E0E0E0");
        inactivo2.TextColor = Colors.Black;
    }
}