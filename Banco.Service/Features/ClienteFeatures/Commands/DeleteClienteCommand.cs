using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.ClienteFeatures.Commands
{
    public class DeleteClienteCommand : IRequest<ResponseDTO>
    {
        public int IdCliente { get; set; }

        public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public DeleteClienteCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Eliminando";

                try
                {
                    var GetCliente = await _context.Clientes.Where(u => u.ClienteId == request.IdCliente)
                        .FirstOrDefaultAsync();

                    if (GetCliente == null)
                    {
                        error = "La Cliente No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetCliente.Estado = GetCliente.Estado == "1" ? "0" :"1";

                    //_context.Clientes.Remove(GetCliente); Caso tal eliminar la info
                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            ClienteId = Convert.ToString(GetCliente.ClienteId)
                        };

                    }
                    else
                    {
                        error = "La Cliente no fue eliminada";
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

