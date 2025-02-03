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
    public class FacturaRepository : GenericRepository<Factura>, IFactura
    {
        private readonly ConexusContext _conexusContext;
        public FacturaRepository(ConexusContext conexusContext) : base(conexusContext)
        {
            _conexusContext = conexusContext;
        }

        public async Task<int> InsertarFactura(Factura factura)
        {
            var parameters = new[]
            {
            new SqlParameter("@Fecha", factura.Fecha),
            new SqlParameter("@Total", factura.Total),
            new SqlParameter("@ClienteId", factura.ClienteId)
            };

            return await _conexusContext.Database.ExecuteSqlRawAsync("EXEC CrearFactura @Fecha, @Total, @ClienteId", parameters);
        }

        public async Task<int> ActualizarFactura(Factura factura)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC EditarFacturas @Id = {0}, @Fecha = {1}, @Total = {2}, @ClienteId = {3}",
                 factura.Id, factura.Fecha, factura.Total, factura.ClienteId
            );
        }

        public async Task<int> EliminarFactura(int id)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC BorrarFactura @Id = {0}", id
            );
        }
    }
}
