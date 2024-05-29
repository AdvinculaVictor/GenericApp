using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericApp.Domain.Entities;
using GenericApp.Domain.Repositories;

namespace GenericApp.DataMan.Repositories.TypeRepositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(GenericAppDBContext context) : base(context) { }
    }
}