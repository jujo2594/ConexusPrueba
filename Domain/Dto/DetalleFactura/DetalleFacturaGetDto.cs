namespace ConexusPruebaAPI.Dto.DetalleFactura
{
    public class DetalleFacturaGetDto
    {
        public int Id{ get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
    }
}
