using SQLite;
using LetyAccesoriosApp.Models;

namespace LetyAccesoriosApp.Data
{
    // Modelos de datos para SQLite
    [Table("Insumos")]
    public class InsumoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public string Unidad { get; set; } = string.Empty;
    }

    [Table("Productos")]
    public class ProductoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }

    [Table("Ventas")]
    public class VentaModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        private decimal _total;
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                _totalVenta = value;
            }
        }

        private decimal _totalVenta;
        public decimal TotalVenta
        {
            get => _totalVenta != 0 ? _totalVenta : _total;
            set
            {
                _totalVenta = value;
                _total = value;
            }
        }

        public string Detalle { get; set; } = string.Empty;
    }

    public class DatabaseContext
    {
        private SQLiteAsyncConnection? _database;

        private async Task InitAsync()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "lety_accesorios.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<InsumoModel>();
            await _database.CreateTableAsync<ProductoModel>();
            await _database.CreateTableAsync<VentaModel>();
            await _database.CreateTableAsync<Producto>();
        }

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            await InitAsync();
            return _database!;
        }

        // --- MÉTODOS PARA PRODUCTOS ---
        public async Task<List<ProductoModel>> GetProductosAsync()
        {
            await InitAsync();
            return await _database!.Table<ProductoModel>().ToListAsync();
        }

        public async Task<int> SaveProductoAsync(ProductoModel producto)
        {
            await InitAsync();
            if (producto.Id != 0)
            {
                return await _database!.UpdateAsync(producto);
            }
            else
            {
                return await _database!.InsertAsync(producto);
            }
        }

        public async Task<int> SaveProductoAsync(Producto producto)
        {
            await InitAsync();
            if (producto.Id != 0)
            {
                return await _database!.UpdateAsync(producto);
            }
            else
            {
                return await _database!.InsertAsync(producto);
            }
        }

        public async Task<int> DeleteProductoAsync(ProductoModel producto)
        {
            await InitAsync();
            return await _database!.DeleteAsync(producto);
        }

        public async Task<int> DeleteProductoAsync(Producto producto)
        {
            await InitAsync();
            return await _database!.DeleteAsync(producto);
        }

        // --- MÉTODOS PARA VENTAS ---
        public async Task<List<VentaModel>> GetVentasByFechaAsync(DateTime fecha)
        {
            await InitAsync();
            DateTime inicioDia = fecha.Date;
            DateTime finDia = fecha.Date.AddDays(1).AddTicks(-1);

            return await _database!.Table<VentaModel>()
                .Where(v => v.Fecha >= inicioDia && v.Fecha <= finDia)
                .ToListAsync();
        }

        public async Task<List<VentaModel>> GetVentasByFechaAsync(DateTime desde, DateTime hasta)
        {
            await InitAsync();
            DateTime inicio = desde.Date;
            DateTime fin = hasta.Date.AddDays(1).AddTicks(-1);

            return await _database!.Table<VentaModel>()
                .Where(v => v.Fecha >= inicio && v.Fecha <= fin)
                .ToListAsync();
        }

        public async Task<int> SaveVentaAsync(VentaModel venta)
        {
            await InitAsync();
            return await _database!.InsertAsync(venta);
        }

        // --- MÉTODOS PARA INSUMOS ---
        public async Task<List<InsumoModel>> GetInsumosAsync()
        {
            await InitAsync();
            return await _database!.Table<InsumoModel>().ToListAsync();
        }

        public async Task<int> SaveInsumoAsync(InsumoModel insumo)
        {
            await InitAsync();
            if (insumo.Id != 0)
            {
                return await _database!.UpdateAsync(insumo);
            }
            else
            {
                return await _database!.InsertAsync(insumo);
            }
        }
    }
}