using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Factura : BaseEntity
    {
        public DateTime Fecha { get; set; } = DateTime.Now; 
        public decimal Total { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
