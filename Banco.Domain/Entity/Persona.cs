using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain.Entity
{
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
