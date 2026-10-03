using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Models; // <-- Añadido para reconocer ProductoVenta

namespace LetyAccesoriosApp.ViewModels
{
    public class StockViewModel : INotifyPropertyChanged
    {
        // Se cambió DatabaseContext por el nuevo DatabaseService centralizado
        private readonly DatabaseService _databaseContext;
        private string _nombre = string.Empty;
        private int _cantidad;
        private bool _isBusy;

        // Se actualizó la colección para usar el modelo correcto: ProductoVenta
        public ObservableCollection<ProductoVenta> Productos { get; } = new ObservableCollection<ProductoVenta>();

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                if (_cantidad != value)
                {
                    _cantidad = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        // Se modificó el constructor para recibir el servicio por Inyección de Dependencias
        public StockViewModel(DatabaseService databaseService)
        {
            _databaseContext = databaseService;
            _nombre = string.Empty;
            _cantidad = 0;
        }

        public async Task CargarProductosAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Productos.Clear();

                // Se actualizó al método oficial de obtención de productos
                var listaProductos = await _databaseContext.ObtenerProductosAsync();
                foreach (var producto in listaProductos)
                {
                    Productos.Add(producto);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar productos: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
