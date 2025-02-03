using ConexusPruebaAPI.Dto.Cliente;
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
        Task<int> InsertarCliente(Cliente cliente);
        Task<int> ActualizarCliente(Cliente cliente);
        Task<int> EliminarCliente(int id);
    }
}
