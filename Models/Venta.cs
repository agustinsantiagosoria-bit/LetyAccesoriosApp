using SQLite;

namespace LetyAccesoriosApp.Models;

public class Venta
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public int CantidadVendida { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal TotalVenta { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.Now;
}