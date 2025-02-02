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
    internal class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(p => p.Precio)
                   .HasColumnType("decimal(12,3)")
                   .IsRequired();

            builder.Property(p => p.Stock)
                   .IsRequired();
        }
    }
}
