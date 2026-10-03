using System;
using SQLite;

namespace LetyAccesoriosApp.Models
{
    public class Venta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double TotalGastadoCosto { get; set; } // Lo que costó producir todo

        public double TotalGanado { get; set; } // Lo que ingresó en caja

        public double GananciaNeta { get; set; } // Margen real de ganancia

        public bool Activa { get; set; } = true; // Permite cancelar la venta si es false
    }
}
