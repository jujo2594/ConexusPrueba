using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProducto : IGenericRepository<Producto>
    {
        Task<int> InsertarProducto(Producto producto);
        Task<int> ActualizarProducto(Producto producto);
        Task<int> EliminarProducto(int id);
    }
}
