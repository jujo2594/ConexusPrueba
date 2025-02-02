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
    internal class FacturaConfiguration : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.ToTable("Facturas");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Fecha)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(f => f.Total)
                   .HasColumnType("decimal(12,3)")
                   .IsRequired();

            builder.HasOne(f => f.Cliente)
                   .WithMany(c => c.Facturas)
                   .HasForeignKey(f => f.ClienteId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
