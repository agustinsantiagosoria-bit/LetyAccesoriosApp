using LetyAccesoriosApp.Views;
using LetyAccesoriosApp.Data;

namespace LetyAccesoriosApp;

public partial class App : Application
{
    // Modificamos el constructor para recibir la base de datos e inyectarla en la página de inicio
    public App(DatabaseService databaseService)
    {
        InitializeComponent();

        // Establecemos que la página inicial real dentro de la navegación sea InsumosPage
        MainPage = new NavigationPage(new InsumosPage(databaseService));
    }
}
