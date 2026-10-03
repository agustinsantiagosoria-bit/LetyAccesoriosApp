namespace LetyAccesoriosApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Captura de errores no controlados para diagnóstico
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception ex = (Exception)args.ExceptionObject;
                System.Diagnostics.Debug.WriteLine($"[CRASH SINCRO] Excepción no controlada: {ex.Message}\n{ex.StackTrace}");
            };

            MainPage = new AppShell();
        }
    }
}