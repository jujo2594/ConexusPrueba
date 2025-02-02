using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICliente Clientes { get; }
        IFactura Facturas { get; }
        IProducto Productos { get; }
        IDetalleFactura DetalleFacturas { get; }
        Task<int> SaveAsync();
    }
}
