using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClienteRepository Cliente
        {
            get;
        }
        IPedidoRepository Pedido
        {
            get;
        }
        IPedidoDetalleRepository PedidoDetalle
        {
            get;
        }
        IProductoRepository Producto
        {
            get;
        }
        int Save();
    }
}