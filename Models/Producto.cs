using SQLite;

namespace LetyAccesoriosApp.Models
{
    [Table("Productos")]
    public class Producto
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
}