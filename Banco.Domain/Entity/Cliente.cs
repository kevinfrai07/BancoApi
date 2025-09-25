using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain.Entity
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string Contrasenia { get; set; } = string.Empty;
        public string? Estado { get; set; }

        // Relación con Persona
        public Persona Persona { get; set; } = null!;
    }
}
