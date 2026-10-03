using LetyAccesoriosApp.ViewModels;
using LetyAccesoriosApp.Data;
using System;

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
            
            if (_viewModel != null)
            {
                try
                {
                    await _viewModel.CargarDatosAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[INSUMOS LOAD ERROR]: {ex.Message}");
                }
            }
        }
    }
}
