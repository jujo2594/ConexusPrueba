using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICliente : IGenericRepository<Cliente>
    {
        //Task<IEnumerable<Cliente>> GetClienteByProcedimiento();
        //Task<IEnumerable<Cliente>> GetClienteByProcedimiento();
    }
}
