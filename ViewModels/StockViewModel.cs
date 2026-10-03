using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LetyAccesoriosApp.Data;

namespace LetyAccesoriosApp.ViewModels
{
    public class StockViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext _databaseContext;
        private string _nombre = string.Empty;
        private int _cantidad;
        private bool _isBusy;

        public ObservableCollection<ProductoModel> Productos { get; } = new ObservableCollection<ProductoModel>();

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

        public StockViewModel()
        {
            _databaseContext = new DatabaseContext();
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

                var listaProductos = await _databaseContext.GetProductosAsync();
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