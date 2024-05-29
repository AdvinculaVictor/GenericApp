using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Domain.Entities
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string? Descripcion { get; set; }
        public string? Marca { get; set; }
        public float Precio { get; set; }
        public int Stock { get; set; }
    }
}