using Domain.Entities;

namespace ConexusPruebaAPI.Dto.DetalleFactura
{
    public class DetalleFacturaDto
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
    }
}
