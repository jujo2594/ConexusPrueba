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
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly ConexusContext _conexusContext;
        public ProductoRepository(ConexusContext conexusContext) : base(conexusContext)
        {
            _conexusContext = conexusContext;
        }

        public async Task<int> InsertarProducto(Producto entity)
        {
            var parameters = new[]
            {
            new SqlParameter("@Nombre", entity.Nombre),
            new SqlParameter("@Precio", entity.Precio),
            new SqlParameter("@Stock", entity.Stock)
            };

            return await _conexusContext.Database.ExecuteSqlRawAsync("EXEC CrearProductos @Nombre, @Precio, @Stock", parameters);
        }

        public async Task<int> ActualizarProducto(Producto producto)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC EditarProductos @Id = {0}, @Nombre = {1}, @Precio = {2}, @Stock = {3}",
                 producto.Id, producto.Nombre, producto.Precio, producto.Stock
            );
        }

        public async Task<int> EliminarProducto(int id)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC BorrarProducto @Id = {0}", id
            );
        }
    }
}
