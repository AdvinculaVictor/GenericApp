using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Domain.Entities
{
    public class PedidoDetalle
    {
        [Key]
        public int PedidoDetalleId { get; set; }
        public int PedidoId { get; set; }
        public int Cantidad { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

    }
}