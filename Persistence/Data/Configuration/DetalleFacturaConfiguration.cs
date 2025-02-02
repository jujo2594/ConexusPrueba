using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configuration
{
    internal class DetalleFacturaConfiguration : IEntityTypeConfiguration<DetalleFactura>
    {
        public void Configure(EntityTypeBuilder<DetalleFactura> builder)
        {
            builder.ToTable("DetalleFacturas");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Cantidad)
                   .IsRequired();

            builder.Property(d => d.PrecioUnitario)
                   .HasColumnType("decimal(12,3)")
                   .IsRequired();

            builder.Property(d => d.Subtotal)
                   .HasColumnType("decimal(12,3)")
                   .IsRequired();

            builder.HasOne(d => d.Factura)
                   .WithMany(f => f.DetallesFactura)
                   .HasForeignKey(d => d.FacturaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Producto)
                   .WithMany(p => p.DetallesFactura)
                   .HasForeignKey(d => d.ProductoId);
        }
    }
}
