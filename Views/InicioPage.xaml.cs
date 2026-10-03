namespace LetyAccesoriosApp.Views;

public partial class InicioPage : ContentPage
{
    public InicioPage()
    {
        InitializeComponent();
    }

    private async void OnIrAProductosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Productos");
    }

    private async void OnIrAVentasClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Ventas");
    }
}