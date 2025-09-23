using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.PersonaFeatures.Commands
{
    public class UpdatePersonaCommand : IRequest<ResponseDTO>
    {
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }

        public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public UpdatePersonaCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Editando Persona";

                try
                {
                    var GetPersona = await _context.Personas.Where(u => u.PersonaId == request.IdPersona)
                        .FirstOrDefaultAsync();

                    if (GetPersona == null)
                    {
                        error = "Persona No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetPersona.Nombre = request.Persona.Nombre;
                    GetPersona.Genero = request.Persona.Genero;
                    GetPersona.Edad = request.Persona.Edad;
                    GetPersona.Identificacion = request.Persona.Identificacion;
                    GetPersona.Direccion = request.Persona.Direccion;
                    GetPersona.Telefono = request.Persona.Telefono;

                    var nroRegPersona = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegPersona > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            PersonaId = Convert.ToString(GetPersona.PersonaId)
                        };

                    }
                    else
                    {
                        error = "Persona no Editada";
                        respuesta.responseStatus = 400;
                        respuesta.responseData = new
                        {
                            error = error
                        };
                    }

                    return respuesta;

                }
                catch (Exception e)
                {
                    var mensaje = $"ERROR No Controlado {e.Message} {e.InnerException}";
                    respuesta.responseStatus = 500;
                    respuesta.responseData = new
                    {
                        error = error
                    };


                    return respuesta;
                }
            }
        }
    }
}

