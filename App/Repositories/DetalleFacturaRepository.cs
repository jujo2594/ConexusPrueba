using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class DetalleFacturaRepository : GenericRepository<DetalleFactura>, IDetalleFactura
    {
        private readonly ConexusContext _conexusContext;
        public DetalleFacturaRepository(ConexusContext conexusContext) : base(conexusContext)
        {
            _conexusContext = conexusContext;
        }
    }
}
