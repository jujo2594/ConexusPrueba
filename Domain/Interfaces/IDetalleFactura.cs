using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDetalleFactura : IGenericRepository<DetalleFactura>
    {
        Task<int> InsertarDetalleFactura(DetalleFactura detalleFactura);
        Task<int> ActualizarDetalleFactura(DetalleFactura detalleFactura);
        Task<int> EliminarDetalleFactura(int id);
    }
}
