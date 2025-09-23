using Banco.Domain.DTO;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.PersonaFeatures.Queries
{
    public class GetPersona : IRequest<ResponseDTO>
    {
        public class GetUsersHandler : IRequestHandler<GetPersona, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetPersona request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por UID";

                try
                {
                    var usuario = await _context.Personas.Select(x=> x)
                        .ToListAsync();
                    object responseData = new object();

                    if (usuario.Count == 0)
                    {
                        error = "Usuarios no encontrados";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            userId = "",
                        };

                        return respuesta;
                    }


                    respuesta.responseStatus = 200;
                    respuesta.responseData = responseData;
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
