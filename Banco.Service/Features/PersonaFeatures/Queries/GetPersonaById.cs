using Banco.Domain.DTO;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.PersonaFeatures.Queries
{
    public class GetPersonaById : IRequest<ResponseDTO>
    {
        public int IdPersona { get; set; }

        public class GetPersonaByIdHandler : IRequestHandler<GetPersonaById, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetPersonaByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetPersonaById request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por UID";

                try
                {
                    var persona = await _context.Personas.Where(x=> x.PersonaId == request.IdPersona)
                        .FirstOrDefaultAsync();
                    if (persona == null)
                    {
                        error = "persona no encontrada";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            PersonaId = "",
                        };

                        return respuesta;
                    }
                    respuesta.responseStatus = 200;
                    respuesta.responseData = new
                    {
                        PersonaId = persona.PersonaId,
                        Nombres = persona.Nombre,
                        Direccion = persona.Direccion,
                        Telefono = persona.Telefono,
                        Genero = persona.Genero,
                        Identificacion = persona.Identificacion,
                    };
                    return respuesta;
                }
                catch (Exception e)
                {
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
