namespace LetyAccesoriosApp.Views;

public partial class ProductoFormPage : ContentPage
{
    private readonly Data.DatabaseContext _database;

    public ProductoFormPage()
    {
        InitializeComponent();
        _database = new Data.DatabaseContext();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
{
    // Validar y parsear las entradas de texto
    if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
        !decimal.TryParse(PrecioEntry.Text, out decimal precio) ||
        !int.TryParse(StockActualEntry.Text, out int stockActual) ||
        !int.TryParse(StockMinimoEntry.Text, out int stockMinimo))
    {
        await DisplayAlert("Error", "Por favor completa todos los campos con valores numéricos válidos.", "OK");
        return;
    }

    // Crear el objeto con la variable 'precio' ya convertida
    var producto = new Models.Producto
    {
        Nombre = NombreEntry.Text,
        Precio = precio, // <--- Debe recibir la variable decimal 'precio', no 'PrecioEntry.Text'
        StockActual = stockActual,
        StockMinimo = stockMinimo
    };

    await _database.SaveProductoAsync(producto);
    await Shell.Current.GoToAsync("..");
}
}