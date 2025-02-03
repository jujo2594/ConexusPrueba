using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFactura : IGenericRepository<Factura>
    {
        Task<int> InsertarFactura(Factura factura);
        Task<int> ActualizarFactura(Factura factura);
        Task<int> EliminarFactura(int id);
    }
}
