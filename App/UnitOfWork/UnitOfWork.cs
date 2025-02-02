using App.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ConexusContext _conexusContext;

        public UnitOfWork(ConexusContext conexusContext)
        {
            _conexusContext = conexusContext;
        }
        private ICliente _clientes;
        private IProducto _productos;
        private IFactura _facturas;
        private IDetalleFactura _detalleFacturas;

        public ICliente Clientes
        {
            get 
            {
                if (_clientes == null)
                {
                    _clientes = new ClienteRepository(_conexusContext);
                }
                return _clientes;
            }
        }

        public IFactura Facturas     
        {
            get 
            {
                if (_facturas == null)
                {
                    _facturas = new FacturaRepository(_conexusContext);
                }
                return _facturas;
            }
        }

        public IProducto Productos
        {
            get
            {
                if (_productos == null)
                {
                    _productos = new ProductoRepository(_conexusContext);
                }
                return _productos;
            }
        }

        public IDetalleFactura DetalleFacturas
        {
            get
            {
                if (_detalleFacturas == null)
                {
                    _detalleFacturas = new DetalleFacturaRepository(_conexusContext);
                }
                return _detalleFacturas;
            }
        }

        public void Dispose()
        {
            _conexusContext.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _conexusContext.SaveChangesAsync();
        }
    }
}
