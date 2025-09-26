using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using Banco.Service.Features.ClienteFeatures.Queries;
using Banco.Service.Features.PersonaFeatures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Banco.Service.Features.ClienteFeatures.Commands
{
    public class CreateClienteCommand : IRequest<ResponseDTO>
    {
        public Cliente Cliente { get; set; }

        public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;
            readonly IMediator _mediator;

            public CreateClienteCommandHandler(
                IApplicationDbContext context,
                IMediator mediator
            )
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<ResponseDTO> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Cliente";

                try
                {
                    

                    //Verifica si ya existe el Cliente registrado
                    var GetCliente = await _context.Clientes.Where(u => u.ClienteId == request.Cliente.ClienteId
                                                                        && u.Contrasenia == request.Cliente.Contrasenia)
                        .FirstOrDefaultAsync();

                    if (GetCliente != null)
                    {
                        error = "Ya existe una Cliente con los datos suministrados.";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    var getPersona = await _mediator.Send(new GetPersonaById { IdPersona = request.Cliente.PersonaId, });

                    Cliente cliente = request.Cliente;



                    _context.Clientes.Add(cliente);

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            ClienteId = Convert.ToString(cliente.ClienteId)
                        };

                    }
                    else
                    {
                        error = "la Cliente no fue creada";
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

