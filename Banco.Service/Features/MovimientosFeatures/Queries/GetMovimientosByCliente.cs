using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Banco.Service.Features.MovimientosFeatures.Queries
{
    public class GetMovimientosByCliente : IRequest<ResponseDTO>
    {
        public int clienteId { get; set; }
        public class GetUsersHandler : IRequestHandler<GetMovimientosByCliente, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public GetUsersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ResponseDTO> Handle(GetMovimientosByCliente request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error en consulta";

                try
                {
                    var listadoMovimientos = await _context.Movimientos
                                            .Join(_context.Cuentas,
                                                m => m.NumeroCuenta,
                                                c => c.NumeroCuenta,
                                                (m, c) => new { m, c })
                                            .Join(_context.Clientes,
                                                temp => temp.c.ClienteId,
                                                cl => cl.ClienteId,
                                                (temp, cl) => new { temp.m, temp.c, cl })
                                            .Where(cl => cl.cl.ClienteId == request.clienteId)
                                            .Join(_context.Personas,
                                                temp => temp.cl.PersonaId,
                                                pe => pe.PersonaId,
                                                (temp, pe) => new MovimientoListadoDTO
                                                {
                                                    Fecha = temp.m.Fecha,
                                                    Cliente = pe.Nombre,
                                                    NumeroCuenta = temp.c.NumeroCuenta,
                                                    TipoCuenta = temp.c.TipoCuenta,
                                                    SaldoInicial = temp.c.SaldoInicial,
                                                    Estado = temp.c.Estado == "1" ? "True" : "False",
                                                    Movimiento = temp.m.TipoMovimiento == "Depósito"
                                                                        ? temp.m.Valor
                                                                        : -temp.m.Valor,
                                                    SaldoDisponible = temp.m.Saldo ?? 0
                                                })
                                            .OrderByDescending(x => x.Fecha)
                                            .ToListAsync();
                    object responseData = new object();

                    if (listadoMovimientos.Count == 0)
                    {
                        error = "Listado de movimientos no encontrados";

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            Movimientos = "",
                        };

                        return respuesta;
                    }


                    respuesta.responseStatus = 200;
                    respuesta.responseData = listadoMovimientos;
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
