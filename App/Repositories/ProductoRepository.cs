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
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly ConexusContext _conexusContext;
        public ProductoRepository(ConexusContext conexusContext) : base(conexusContext)
        {
            _conexusContext = conexusContext;
        }
    }
}
