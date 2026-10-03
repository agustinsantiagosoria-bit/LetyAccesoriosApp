using LetyAccesoriosApp.ViewModels;
using LetyAccesoriosApp.Data;

namespace LetyAccesoriosApp.Views
{
    public partial class InsumosPage : ContentPage
    {
        private readonly InsumosViewModel _viewModel;

        // El constructor recibe el inyector de dependencias de la base de datos
        public InsumosPage(DatabaseService databaseService)
        {
            InitializeComponent();
            
            // Instanciamos el intermediario de datos y lo asignamos a la vista
            _viewModel = new InsumosViewModel(databaseService);
            BindingContext = _viewModel;
        }
        // Recarga los datos automáticamente al entrar a la pantalla
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (_viewModel != null)
            {
                await _viewModel.CargarDatosAsync();
            }
        }
    }
}
