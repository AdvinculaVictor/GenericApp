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
        public string RazonSocial { get; set; }
        public string? Calle { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string? Estado { get; set; }
        public int CP { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? RFC { get; set; }
        public List<Pedido>? Pedidos { get; set; }
    }
}