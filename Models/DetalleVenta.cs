using SQLite;

namespace LetyAccesoriosApp.Models
{
    public class DetalleVenta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int VentaId { get; set; }

        public int ProductoVentaId { get; set; }

        public int Cantidad { get; set; }

        public double PrecioUnitarioVenta { get; set; }
    }
}
