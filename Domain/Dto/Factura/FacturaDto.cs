namespace ConexusPruebaAPI.Dto.Factura
{
    public class FacturaDto
    {
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int ClienteId { get; set; }
    }
}
