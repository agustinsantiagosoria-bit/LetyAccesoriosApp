namespace LetyAccesoriosApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Establecemos el inicio nativo estándar usando el AppShell centralizado
        MainPage = new AppShell();
    }
}
