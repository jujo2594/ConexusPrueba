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
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nombre)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(c => c.Correo)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(c => c.Telefono)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
