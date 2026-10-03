using SQLite;

namespace LetyAccesoriosApp.Models
{
    public class Sucursal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Unique, NotNull]
        public string Nombre { get; set; }
    }
}
