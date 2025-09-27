using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.CuentaFeatures.Commands
{
    public class UpdateCuentaCommand : IRequest<ResponseDTO>
    {
        public string NumeroCuenta { get; set; }
        public Cuenta Cuenta { get; set; }

        public class UpdateCuentaCommandHandler : IRequestHandler<UpdateCuentaCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCuentaCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(UpdateCuentaCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error Editando Cuenta";

                try
                {
                    var GetCuenta = await _context.Cuentas.Where(u => u.NumeroCuenta == request.NumeroCuenta)
                        .FirstOrDefaultAsync();

                    if (GetCuenta == null)
                    {
                        error = "Cuenta No existe";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    GetCuenta.Estado = request.Cuenta.Estado;
                    GetCuenta.ClienteId = request.Cuenta.ClienteId;
                    GetCuenta.TipoCuenta = request.Cuenta.TipoCuenta;
                    GetCuenta.SaldoInicial = request.Cuenta.SaldoInicial;
                    GetCuenta.Estado = request.Cuenta.Estado;

                    _context.Cuentas.Update(GetCuenta);

                    var nroRegCuenta = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegCuenta > 0)
                    {
                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            CuentaId = Convert.ToString(GetCuenta.NumeroCuenta)
                        };

                    }
                    else
                    {
                        error = "Cuenta no Editada";
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

