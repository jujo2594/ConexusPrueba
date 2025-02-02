using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Data
{
    public class ConexusContext : DbContext
    {
        public ConexusContext(DbContextOptions<ConexusContext> options) : base(options)
        {
            
        }
        public DbSet<Cliente> Clientes{ get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }

    }

}
