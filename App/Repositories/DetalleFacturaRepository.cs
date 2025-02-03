using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class DetalleFacturaRepository : GenericRepository<DetalleFactura>, IDetalleFactura
    {
        private readonly ConexusContext _conexusContext;
        public DetalleFacturaRepository(ConexusContext conexusContext) : base(conexusContext)
        {
            _conexusContext = conexusContext;
        }

        public async Task<int> InsertarDetalleFactura(DetalleFactura detalleFactura)
        {
            var parameters = new[]
            {
            new SqlParameter("@Cantidad", detalleFactura.Cantidad),
            new SqlParameter("@PrecioUnitario", detalleFactura.PrecioUnitario),
            new SqlParameter("@Subtotal", detalleFactura.Subtotal),
            new SqlParameter("@FacturaId", detalleFactura.FacturaId),
            new SqlParameter("@ProductoId", detalleFactura.ProductoId),
            };

            return await _conexusContext.Database
                .ExecuteSqlRawAsync("EXEC CrearDetalleFactura @Cantidad, @PrecioUnitario, @Subtotal, @FacturaId, @ProductoId", parameters);
        }

        public async Task<int> ActualizarDetalleFactura(DetalleFactura detalleFactura)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC EditarDetalleFactura @Id = {0}, @Cantidad = {1}, @PrecioUnitario = {2}, @Subtotal = {3}, @FacturaId = {4}, @ProductoId = {5}", 
                 detalleFactura.Id, detalleFactura.Cantidad, detalleFactura.PrecioUnitario, detalleFactura.Subtotal, detalleFactura.FacturaId, detalleFactura.ProductoId
            );
        }

        public async Task<int> EliminarDetalleFactura(int id)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC BorrarDetalleFactura @Id = {0}", id
            );
        }
    }
}
