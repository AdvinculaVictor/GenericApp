using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.DataMan.Entities
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<PedidoDetalle>? PedidoDetalles { get; set; }
    }
}