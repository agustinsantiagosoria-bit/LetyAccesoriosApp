using LetyAccesoriosApp.ViewModels;
using LetyAccesoriosApp.Data;

namespace LetyAccesoriosApp.Views
{
    public partial class InsumosPage : ContentPage
    {
        private readonly InsumosViewModel _viewModel;

        public InsumosPage(DatabaseService databaseService)
        {
            InitializeComponent();
            
            _viewModel = new InsumosViewModel(databaseService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarDatosAsync();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Evitamos ejecutar lógica si la ventana gráfica no se terminó de dibujar
            if (MainContainer == null || FormPanel == null || ListPanel == null)
                return;

            // EFECTO RESPONSIVO: Si la pantalla mide más de 750px (Modo PC / Monitor)
            if (width > 750)
            {
                MainContainer.ColumnDefinitions = new ColumnDefinitionCollection 
                { 
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) }
                };
                MainContainer.RowDefinitions = new RowDefinitionCollection { new RowDefinition { Height = GridLength.Auto } };

                // Colocamos el formulario a la izquierda (Columna 0)
                Grid.SetRow(FormPanel, 0);
                Grid.SetColumn(FormPanel, 0);
                
                // Colocamos el listado de stock a la derecha (Columna 1)
                Grid.SetRow(ListPanel, 0);
                Grid.SetColumn(ListPanel, 1);
            }
            else // Modo Celular (Apilado vertical tradicional)
            {
                MainContainer.ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition { Width = GridLength.Star } };
                MainContainer.RowDefinitions = new RowDefinitionCollection 
                { 
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                };

                Grid.SetRow(FormPanel, 0);
                Grid.SetColumn(FormPanel, 0);
                
                Grid.SetRow(ListPanel, 1);
                Grid.SetColumn(ListPanel, 0);
            }
        }
    }
}
