using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.MovimientosFeatures.Queries
{
    public class GetMovimientos : IRequest<ResponseDTO>
    {
        public class GetUsersHandler : IRequestHandler<GetMovimientos, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetMovimientos request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error consultando tabla Movimientos";

                try
                {
                    var movimientos = await _context.Movimientos
                                    .Join(_context.Cuentas,
                                        m => m.NumeroCuenta,
                                        c => c.NumeroCuenta,
                                        (m, c) => new { m, c })
                                    .Join(_context.Clientes,
                                        temp => temp.c.ClienteId,
                                        cl => cl.ClienteId,
                                        (temp, cl) => new { temp.m, temp.c, cl })
                                    .Join(_context.Personas,
                                        temp => temp.cl.PersonaId,
                                        pe => pe.PersonaId,
                                        (temp, pe) => new MovimientoDTO
                                        {
                                            NumeroCuenta = temp.c.NumeroCuenta,
                                            TipoCuenta = temp.c.TipoCuenta,
                                            SaldoInicial = temp.c.SaldoInicial,
                                            Estado = temp.c.Estado,
                                            Movimiento = $"{temp.m.TipoMovimiento} de {temp.m.Valor}", // 👈 texto del movimiento
                                            Cliente = pe.Nombre
                                        })
                                    .ToListAsync();
                    object responseData = new object();

                    if (movimientos.Count == 0)
                    {
                        error = "Movimientoss no encontrados";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            Movimientos = "",
                        };

                        return respuesta;
                    }


                    respuesta.responseStatus = 200;
                    respuesta.responseData = movimientos;
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
