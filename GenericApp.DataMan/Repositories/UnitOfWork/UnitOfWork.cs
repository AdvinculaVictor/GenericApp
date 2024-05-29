using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericApp.DataMan.Repositories.TypeRepositories;
using GenericApp.Domain.Repositories;

namespace GenericApp.DataMan.Repositories.UnitOfWork
{
public class UnitOfWork: IUnitOfWork {
    private GenericAppDBContext context;
    public UnitOfWork(GenericAppDBContext context) {
        this.context = context;
        Cliente = new ClienteRepository(this.context);
        Pedido = new PedidoRepository(this.context);
        PedidoDetalle = new PedidoDetalleRepository(this.context);
        Producto = new ProductoRepository(this.context);
    }
    public IClienteRepository Cliente {
        get;
        private set;
    }
    public IPedidoRepository Pedido {
        get;
        private set;
    }
    public IPedidoDetalleRepository PedidoDetalle {
        get;
        private set;
    }
    public IProductoRepository Producto {
        get;
        private set;
    }    
    public void Dispose() {
        context.Dispose();
    }
    public int Save() {
        return context.SaveChanges();
    }
}
}