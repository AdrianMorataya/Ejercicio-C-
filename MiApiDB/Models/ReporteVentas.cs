using System;
using System.Collections.Generic;

namespace MiApiDB.Models
{
    public class ReporteVentas
    {
        public decimal TotalVentas { get; set; }
        public decimal TotalInsumos { get; set; }
        public decimal TotalProductos { get; set; }
        public decimal Ganancia => TotalVentas - (TotalInsumos + TotalProductos);
        public List<Venta> Ventas { get; set; }
    }
}
