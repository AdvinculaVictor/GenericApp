using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericApp.Domain.Entities;
using GenericApp.Domain.Repositories;

namespace GenericApp.DataMan.Repositories.TypeRepositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(GenericAppDBContext context) : base(context) { }
    }
}