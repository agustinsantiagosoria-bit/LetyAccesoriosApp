using LetyAccesoriosApp.Views;

namespace LetyAccesoriosApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registro de rutas para la navegación de la Shell
            Routing.RegisterRoute("InsumosPage", typeof(StockPage));
            Routing.RegisterRoute("ProductosPage", typeof(StockPage));
            Routing.RegisterRoute("VentasPage", typeof(VentasPage));
            Routing.RegisterRoute("ResumenPage", typeof(VentasPage));
            Routing.RegisterRoute("ProductoFormPage", typeof(ProductoFormPage));
        }
    }
}