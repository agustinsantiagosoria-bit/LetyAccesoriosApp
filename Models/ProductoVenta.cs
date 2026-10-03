using SQLite;

namespace LetyAccesoriosApp.Models
{
    public class ProductoVenta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; }

        public double CostoProduccion { get; set; }

        public double PrecioVenta { get; set; }

        public double PorcentajeGanancia { get; set; }
    }
}
