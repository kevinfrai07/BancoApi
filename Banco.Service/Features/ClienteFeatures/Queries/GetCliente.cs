using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.ClienteFeatures.Queries
{
    public class GetCliente : IRequest<ResponseDTO>
    {
        public class GetUsersHandler : IRequestHandler<GetCliente, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetCliente request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando tabla cliente";

                try
                {
                    var usuario = await _context.Clientes
                        .Join(_context.Personas,
                                c => c.PersonaId,
                                p => p.PersonaId,
                                (c, p) => new ClienteDTO
                                {
                                    ClienteId = c.ClienteId,
                                    Contrasenia = c.Contrasenia,
                                    Estado = c.Estado,
                                    Persona = new Persona
                                    {
                                        PersonaId = p.PersonaId,
                                        Nombre = p.Nombre,
                                        Direccion = p.Direccion,
                                        Telefono = p.Telefono
                                    }
                                    
                                })
                        .ToListAsync();
                    object responseData = new object();

                    if (usuario.Count == 0)
                    {
                        error = "Clientes no encontrados";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            cliente = "",
                        };

                        return respuesta;
                    }


                    respuesta.responseStatus = 200;
                    respuesta.responseData = usuario;
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
