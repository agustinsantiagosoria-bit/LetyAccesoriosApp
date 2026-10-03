namespace LetyAccesoriosApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }

    public class MainPageViewModel
    {
        public Command NavigateToInsumosCommand { get; }
        public Command NavigateToProductosCommand { get; }
        public Command NavigateToVentasCommand { get; }
        public Command NavigateToResumenCommand { get; }
        public Command NavigateToHomeCommand { get; }

        public MainPageViewModel()
        {
            NavigateToHomeCommand = new Command(async () => await NavigateToAsync("//MainPage"));
            NavigateToInsumosCommand = new Command(async () => await NavigateToAsync("//InsumosPage"));
            NavigateToProductosCommand = new Command(async () => await NavigateToAsync("//ProductosPage"));
            NavigateToVentasCommand = new Command(async () => await NavigateToAsync("//VentasPage"));
            NavigateToResumenCommand = new Command(async () => await NavigateToAsync("//ResumenPage"));
        }

        private async Task NavigateToAsync(string pageRoute)
        {
            try
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync(pageRoute);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error de navegación: {ex.Message}");
            }
        }
    }
}