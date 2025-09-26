using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.ClienteFeatures.Queries
{
    public class GetClienteById : IRequest<ResponseDTO>
    {
        public int IdCliente { get; set; }

        public class GetClienteByIdHandler : IRequestHandler<GetClienteById, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetClienteByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetClienteById request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por UID";

                try
                {
                    var cliente = await _context.Clientes
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
                                    }

                                })
                        .Where(x=> x.ClienteId == request.IdCliente)
                        .FirstOrDefaultAsync();
                    if (cliente == null)
                    {
                        error = "Cliente no encontrada";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            ClienteId = "",
                        };

                        return respuesta;
                    }
                    respuesta.responseStatus = 200;
                    respuesta.responseData = cliente;
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
