using ConexusPruebaAPI.Dto.Cliente;
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
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        private readonly ConexusContext _conexusContext;
        public ClienteRepository(ConexusContext conexusContext) : base(conexusContext)
        {
            _conexusContext = conexusContext;
        }

        public async Task<int> InsertarCliente(Cliente entity)
        {
            var parameters = new[]
            {
            new SqlParameter("@Nombre", entity.Nombre),
            new SqlParameter("@Correo", entity.Correo),
            new SqlParameter("@Telefono", entity.Telefono)
            };

            return await _conexusContext.Database.ExecuteSqlRawAsync("EXEC CrearClientes @Nombre, @Correo, @Telefono", parameters);
        }

        public async Task<int> ActualizarCliente(Cliente cliente)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC EditarClientes @Id = {0}, @Nombre = {1}, @Correo = {2}, @Telefono = {3}",
                 cliente.Id, cliente.Nombre, cliente.Correo, cliente.Telefono
            );
        }

        public async Task<int> EliminarCliente(int id)
        {
            return await _conexusContext.Database.ExecuteSqlRawAsync(
                "EXEC BorrarCliente @Id = {0}", id
            );
        }
    }
}
