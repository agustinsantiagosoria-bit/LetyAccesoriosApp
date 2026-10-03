using SQLite;

namespace LetyAccesoriosApp.Models
{
    public class ProductoInsumo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int ProductoVentaId { get; set; }

        [Indexed]
        public int InsumoId { get; set; }

        public double CantidadUtilizada { get; set; }
    }
}
