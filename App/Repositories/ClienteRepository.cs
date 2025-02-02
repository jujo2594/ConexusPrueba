using Domain.Entities;
using Domain.Interfaces;
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

        //public async Task<IEnumerable<Cliente>> GetClienteByProcedimiento()
        //{
        //    var users = await _conexusContext.Clientes.FromSqlRaw("EXECUTE ObtenerClientes").ToListAsync();
        //    return users;
        //}
    }
}
