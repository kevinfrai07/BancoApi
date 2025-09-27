using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Banco.Service.Features.MovimientoFeatures.Commands
{
    public class CreateMovimientosCommand : IRequest<ResponseDTO>
    {
        public Movimiento Movimiento { get; set; }

        public class CreateMovimientoCommandHandler : IRequestHandler<CreateMovimientosCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;
            readonly IMediator _mediator;

            public CreateMovimientoCommandHandler(
                IApplicationDbContext context,
                IMediator mediator
            )
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<ResponseDTO> Handle(CreateMovimientosCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Movimiento";

                try
                {
                    Movimiento Movimiento = request.Movimiento;

                    _context.Movimientos.Add(Movimiento);

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            MovimientoId = Convert.ToString(Movimiento.MovimientoId)
                        };

                    }
                    else
                    {
                        error = "la Movimiento no fue creada";
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

