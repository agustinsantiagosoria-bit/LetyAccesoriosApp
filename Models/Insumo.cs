using SQLite;

namespace LetyAccesoriosApp.Models
{
    public class Insumo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; }

        public double Stock { get; set; }

        public double PrecioCompra { get; set; }

        // Relación con la sucursal (guarda el ID de la Sucursal donde se compró)
        public int SucursalId { get; set; }
        
        // Propiedad de conveniencia para mostrar el nombre sin complicar las consultas
        [Ignore]
        public string NombreSucursal { get; set; }
    }
}
