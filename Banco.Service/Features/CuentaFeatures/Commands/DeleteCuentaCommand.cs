using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.CuentaFeatures.Commands
{
    public class DeleteCuentaCommand : IRequest<ResponseDTO>
    {
        public string NumeroCuenta { get; set; }

        public class DeleteCuentaCommandHandler : IRequestHandler<DeleteCuentaCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCuentaCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(DeleteCuentaCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Eliminando";

                try
                {
                    var GetCuenta = await _context.Cuentas.Where(u => u.NumeroCuenta == request.NumeroCuenta)
                        .FirstOrDefaultAsync();

                    if (GetCuenta == null)
                    {
                        error = "La Cuenta No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetCuenta.Estado = GetCuenta.Estado == "1" ? "0" :"1";

                    //_context.Cuentas.Remove(GetCuenta); Caso tal eliminar la info
                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            CuentaId = Convert.ToString(GetCuenta.NumeroCuenta)
                        };

                    }
                    else
                    {
                        error = "La Cuenta no fue eliminada";
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

