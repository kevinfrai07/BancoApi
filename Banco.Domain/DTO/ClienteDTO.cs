using Banco.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Banco.Domain.DTO
{
    public class ClienteDTO
    {
        [Key]
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string Contrasenia { get; set; } = string.Empty;
        public string? Estado { get; set; } // 0 inactivo 1 activo
        public Persona Persona { get; set; } = null!;

    }
}
