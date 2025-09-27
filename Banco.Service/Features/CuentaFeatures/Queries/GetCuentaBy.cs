using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.CuentaFeatures.Queries
{
    public class GetCuentaById : IRequest<ResponseDTO>
    {
        public string NumeroCuenta { get; set; }

        public class GetCuentaByIdHandler : IRequestHandler<GetCuentaById, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetCuentaByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetCuentaById request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando por UID";

                try
                {
                    var Cuenta = await _context.Cuentas
                                .Join(_context.Clientes,
                                    cu => cu.ClienteId,
                                    cl => cl.ClienteId,
                                    (cu, cl) => new { cu, cl })
                                .Join(_context.Personas,
                                    temp => temp.cl.PersonaId,
                                    pe => pe.PersonaId,
                                    (temp, pe) => new CuentaDTO
                                    {
                                        NumeroCuenta = temp.cu.NumeroCuenta,
                                        TipoCuenta = temp.cu.TipoCuenta,
                                        SaldoInicial = temp.cu.SaldoInicial,
                                        Estado = temp.cu.Estado,
                                        Cliente = pe.Nombre
                                    })
                        .Where(x=> x.NumeroCuenta == request.NumeroCuenta)
                        .FirstOrDefaultAsync();
                    if (Cuenta == null)
                    {
                        error = "Cuenta no encontrada";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            CuentaId = "",
                        };

                        return respuesta;
                    }
                    respuesta.responseStatus = 200;
                    respuesta.responseData = Cuenta;
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
