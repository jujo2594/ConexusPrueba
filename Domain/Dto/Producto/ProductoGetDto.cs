namespace ConexusPruebaAPI.Dto.Producto
{
    public class ProductoGetDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
