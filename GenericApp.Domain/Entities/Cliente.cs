using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string? RazonSocial { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Calle { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Colonia { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Municipio { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Estado { get; set; }
        public int CP { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string? Telefono { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Email { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string? RFC { get; set; }
        public List<Pedido>? Pedidos { get; set; }
    }
}