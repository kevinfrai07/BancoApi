using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.ClienteFeatures.Commands
{
    public class UpdateClienteCommand : IRequest<ResponseDTO>
    {
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public UpdateClienteCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Editando Cliente";

                try
                {
                    var GetCliente = await _context.Clientes.Where(u => u.ClienteId == request.IdCliente)
                        .FirstOrDefaultAsync();

                    if (GetCliente == null)
                    {
                        error = "Cliente No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetCliente.Estado = request.Cliente.Estado;
                    GetCliente.Contrasenia = request.Cliente.Contrasenia;

                    _context.Clientes.Update(GetCliente);

                    var nroRegCliente = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegCliente > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            ClienteId = Convert.ToString(GetCliente.ClienteId)
                        };

                    }
                    else
                    {
                        error = "Cliente no Editada";
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

