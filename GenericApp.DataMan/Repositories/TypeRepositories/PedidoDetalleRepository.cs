using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericApp.Domain.Entities;
using GenericApp.Domain.Repositories;

namespace GenericApp.DataMan.Repositories.TypeRepositories
{
    public class PedidoDetalleRepository : GenericRepository<PedidoDetalle>, IPedidoDetalleRepository
    {
        public PedidoDetalleRepository(GenericAppDBContext context) : base(context) { }
    }
}