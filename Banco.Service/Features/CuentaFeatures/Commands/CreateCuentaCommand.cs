using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using Banco.Service.Features.ClienteFeatures.Queries;
using Banco.Service.Features.CuentaFeatures.Queries;
using Banco.Service.Features.PersonaFeatures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Banco.Service.Features.CuentaFeatures.Commands
{
    public class CreateCuentaCommand : IRequest<ResponseDTO>
    {
        public Cuenta Cuenta { get; set; }

        public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;
            readonly IMediator _mediator;

            public CreateCuentaCommandHandler(
                IApplicationDbContext context,
                IMediator mediator
            )
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<ResponseDTO> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Cuenta";

                try
                {
                    

                    //Verifica si ya existe el Cuenta registrado
                    var GetCuenta = await _context.Cuentas.Where(u => u.NumeroCuenta == request.Cuenta.NumeroCuenta)
                        .FirstOrDefaultAsync();

                    if (GetCuenta != null)
                    {
                        error = "Ya existe una Cuenta con los datos suministrados.";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }

                    Cuenta Cuenta = request.Cuenta;

                    _context.Cuentas.Add(Cuenta);

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            CuentaId = Convert.ToString(Cuenta.NumeroCuenta)
                        };

                    }
                    else
                    {
                        error = "la Cuenta no fue creada";
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

