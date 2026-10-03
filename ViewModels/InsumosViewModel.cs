using System.Collections.ObjectModel;
using System.Windows.Input;
using LetyAccesoriosApp.Models;
using LetyAccesoriosApp.Data;

namespace LetyAccesoriosApp.ViewModels
{
    public class InsumosViewModel : BindableObject
    {
        private readonly DatabaseService _databaseService;

        // Colecciones para llenar las listas y selectores de la pantalla
        public ObservableCollection<Insumo> Insumos { get; set; } = new();
        public ObservableCollection<Sucursal> Sucursales { get; set; } = new();

        // Propiedades para los campos de texto del formulario de Insumos
        private string _nombreInsumo;
        public string NombreInsumo
        {
            get => _nombreInsumo;
            set { _nombreInsumo = value; OnPropertyChanged(); }
        }

        private double _stockInsumo;
        public double StockInsumo
        {
            get => _stockInsumo;
            set { _stockInsumo = value; OnPropertyChanged(); }
        }

        private double _precioInsumo;
        public double PrecioInsumo
        {
            get => _precioInsumo;
            set { _precioInsumo = value; OnPropertyChanged(); }
        }

        private Sucursal _sucursalSeleccionada;
        public Sucursal SucursalSeleccionada
        {
            get => _sucursalSeleccionada;
            set { _sucursalSeleccionada = value; OnPropertyChanged(); }
        }

        // Propiedad para el formulario de Nueva Sucursal
        private string _nombreNuevaSucursal;
        public string NombreNuevaSucursal
        {
            get => _nombreNuevaSucursal;
            set { _nombreNuevaSucursal = value; OnPropertyChanged(); }
        }

        // Comandos para los botones
        public ICommand CargarDatosCommand { get; }
        public ICommand GuardarInsumoCommand { get; }
        public ICommand GuardarSucursalCommand { get; }

        public InsumosViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            CargarDatosCommand = new Command(async () => await CargarDatosAsync());
            GuardarInsumoCommand = new Command(async () => await GuardarInsumoAsync());
            GuardarSucursalCommand = new Command(async () => await GuardarSucursalAsync());
        }

        public async Task CargarDatosAsync()
        {
            // Limpiamos y recargamos las sucursales
            Sucursales.Clear();
            var sucs = await _databaseService.ObtenerSucursalesAsync();
            foreach (var s in sucs) Sucursales.Add(s);

            // Limpiamos y recargamos los insumos
            Insumos.Clear();
            var ins = await _databaseService.ObtenerInsumosAsync();
            foreach (var i in ins) Insumos.Add(i);
        }

        private async Task GuardarInsumoAsync()
        {
            if (string.IsNullOrWhiteSpace(NombreInsumo) || SucursalSeleccionada == null)
                return;

            var nuevoInsumo = new Insumo
            {
                Nombre = NombreInsumo,
                Stock = StockInsumo,
                PrecioCompra = PrecioInsumo,
                SucursalId = SucursalSeleccionada.Id
            };

            await _databaseService.GuardarInsumoAsync(nuevoInsumo);

            // Limpiamos el formulario y refrescamos la lista
            NombreInsumo = string.Empty;
            StockInsumo = 0;
            PrecioInsumo = 0;
            SucursalSeleccionada = null;

            await CargarDatosAsync();
        }

        private async Task GuardarSucursalAsync()
        {
            if (string.IsNullOrWhiteSpace(NombreNuevaSucursal))
                return;

            var nuevaSuc = new Sucursal { Nombre = NombreNuevaSucursal };
            await _databaseService.GuardarSucursalAsync(nuevaSuc);

            NombreNuevaSucursal = string.Empty;
            await CargarDatosAsync();
        }
    }
}
