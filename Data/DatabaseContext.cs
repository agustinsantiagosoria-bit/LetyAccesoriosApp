using SQLite;
using LetyAccesoriosApp.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LetyAccesoriosApp.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
        }

        private async Task Init()
        {
            if (_database != null)
                return;

            // Define una ruta multiplataforma segura para guardar la base de datos
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "LetyAccesoriosDB.db3");
            
            _database = new SQLiteAsyncConnection(dbPath);

            // Creamos todas las tablas necesarias de forma automática
            await _database.CreateTableAsync<Sucursal>();
            await _database.CreateTableAsync<Insumo>();
            await _database.CreateTableAsync<ProductoVenta>();
            await _database.CreateTableAsync<ProductoInsumo>();
            await _database.CreateTableAsync<Venta>();
            await _database.CreateTableAsync<DetalleVenta>();
        }

        // ==========================================
        // MÓDULO 1: APARTADO DE INSUMOS Y SUCURSALES
        // ==========================================

        public async Task<int> GuardarSucursalAsync(Sucursal sucursal)
        {
            await Init();
            if (sucursal.Id != 0)
                return await _database.UpdateAsync(sucursal);
            else
                return await _database.InsertAsync(sucursal);
        }

        public async Task<List<Sucursal>> ObtenerSucursalesAsync()
        {
            await Init();
            return await _database.Table<Sucursal>().ToListAsync();
        }

        public async Task<int> GuardarInsumoAsync(Insumo insumo)
        {
            await Init();
            if (insumo.Id != 0)
                return await _database.UpdateAsync(insumo);
            else
                return await _database.InsertAsync(insumo);
        }

        public async Task<List<Insumo>> ObtenerInsumosAsync()
        {
            await Init();
            var insumos = await _database.Table<Insumo>().ToListAsync();
            var sucursales = await ObtenerSucursalesAsync();

            // Mapeamos el nombre de la sucursal de forma manual para evitar joins pesados
            foreach (var insumo in insumos)
            {
                var suc = sucursales.Find(s => s.Id == insumo.SucursalId);
                insumo.NombreSucursal = suc != null ? suc.Nombre : "Desconocida";
            }
            return insumos;
        }

        // ==========================================
        // MÓDULO 2: ARMADO DE PRODUCCIONES Y COSTOS
        // ==========================================

        public async Task GuardarProductoConRecetaAsync(ProductoVenta producto, List<ProductoInsumo> receta)
        {
            await Init();
            
            // Usamos una transacción para asegurarnos de que se guarde todo o nada
            await _database.RunInTransactionAsync(tran =>
            {
                if (producto.Id != 0)
                {
                    tran.Update(producto);
                    // Borramos la receta anterior para reescribirla de cero si se modificó
                    var viejosInsumos = tran.Table<ProductoInsumo>().Where(pi => pi.ProductoVentaId == producto.Id).ToList();
                    foreach (var vi in viejosInsumos) tran.Delete(vi);
                }
                else
                {
                    tran.Insert(producto); // Inserta y asigna el Id automáticamente
                }

                foreach (var item in receta)
                {
                    item.ProductoVentaId = producto.Id;
                    tran.Insert(item);
                }
            });
        }

        public async Task<List<ProductoVenta>> ObtenerProductosAsync()
        {
            await Init();
            return await _database.Table<ProductoVenta>().ToListAsync();
        }

        public async Task<List<ProductoInsumo>> ObtenerRecetaDeProductoAsync(int productoId)
        {
            await Init();
            return await _database.Table<ProductoInsumo>().Where(pi => pi.ProductoVentaId == productoId).ToListAsync();
        }

        // ==========================================
        // MÓDULO 3: REGISTRO Y CANCELACIÓN DE VENTAS
        // ==========================================

        public async Task RegistrarVentaAsync(List<DetalleVenta> carrito)
        {
            await Init();

            double totalCosto = 0;
            double totalVenta = 0;

            // Calculamos totales y descontamos stock de insumos por cada producto vendido
            foreach (var item in carrito)
            {
                var prod = await _database.Table<ProductoVenta>().Where(p => p.Id == item.ProductoVentaId).FirstOrDefaultAsync();
                if (prod != null)
                {
                    totalCosto += (prod.CostoProduccion * item.Cantidad);
                    totalVenta += (item.PrecioUnitarioVenta * item.Cantidad);

                    // Descontar los insumos del stock real
                    var receta = await ObtenerRecetaDeProductoAsync(prod.Id);
                    foreach (var recetaItem in receta)
                    {
                        var insumo = await _database.Table<Insumo>().Where(i => i.Id == recetaItem.InsumoId).FirstOrDefaultAsync();
                        if (insumo != null)
                        {
                            insumo.Stock -= (recetaItem.CantidadUtilizada * item.Cantidad);
                            await _database.UpdateAsync(insumo);
                        }
                    }
                }
            }

            Venta nuevaVenta = new Venta
            {
                Fecha = DateTime.Now,
                TotalGastadoCosto = totalCosto,
                TotalGanado = totalVenta,
                GananciaNeta = totalVenta - totalCosto,
                Activa = true
            };

            await _database.InsertAsync(nuevaVenta);

            foreach (var item in carrito)
            {
                item.VentaId = nuevaVenta.Id;
                await _database.InsertAsync(item);
            }
        }

        public async Task CancelarVentaAsync(int ventaId)
        {
            await Init();
            var venta = await _database.Table<Venta>().Where(v => v.Id == ventaId).FirstOrDefaultAsync();
            
            if (venta != null && venta.Activa)
            {
                venta.Activa = false;
                await _database.UpdateAsync(venta);

                // Devolvemos los insumos al stock original
                var detalles = await _database.Table<DetalleVenta>().Where(dv => dv.VentaId == ventaId).ToListAsync();
                foreach (var detalle in detalles)
                {
                    var receta = await ObtenerRecetaDeProductoAsync(detalle.ProductoVentaId);
                    foreach (var recetaItem in receta)
                    {
                        var insumo = await _database.Table<Insumo>().Where(i => i.Id == recetaItem.InsumoId).FirstOrDefaultAsync();
                        if (insumo != null)
                        {
                            insumo.Stock += (recetaItem.CantidadUtilizada * detalle.Cantidad);
                            await _database.UpdateAsync(insumo);
                        }
                    }
                }
            }
        }

        public async Task<List<Venta>> ObtenerHistorialVentasAsync()
        {
            await Init();
            return await _database.Table<Venta>().OrderByDescending(v => v.Fecha).ToListAsync();
        }
    }
}
