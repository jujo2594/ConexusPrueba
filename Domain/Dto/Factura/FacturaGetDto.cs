namespace ConexusPruebaAPI.Dto.Factura
{
    public class FacturaGetDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int ClienteId { get; set; }
    }
}
