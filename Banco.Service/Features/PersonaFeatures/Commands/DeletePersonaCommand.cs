using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.PersonaFeatures.Commands
{
    public class DeletePersonaCommand : IRequest<ResponseDTO>
    {
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }

        public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public DeletePersonaCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Eliminando";

                try
                {
                    var GetPersona = await _context.Personas.Where(u => u.PersonaId == request.IdPersona)
                        .FirstOrDefaultAsync();

                    if (GetPersona == null)
                    {
                        error = "La persona No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    //GetPersona.estado = 0; caso tal de que tenga un estado

                    //_context.Personas.Remove(GetPersona); Caso tal eliminar la info
                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            PersonaId = Convert.ToString(GetPersona.PersonaId)
                        };

                    }
                    else
                    {
                        error = "La persona no fue eliminada";
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

