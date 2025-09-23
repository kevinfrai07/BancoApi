using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Banco.Service.Features.PersonaFeatures.Commands
{
    public class CreatePersonaCommand : IRequest<ResponseDTO>
    {
        public Persona Persona { get; set; }
        public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, ResponseDTO>
        {
            private readonly IApplicationDbContext _context;

            public CreatePersonaCommandHandler(
                IApplicationDbContext context
            )
            {
                _context = context;
            }
            public async Task<ResponseDTO> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
            {
                ResponseDTO respuesta = new ResponseDTO();
                string error = "Error creando Persona";

                try
                {
                    

                    //Verifica si ya existe el Persona registrado
                    var GetPersona = await _context.Personas.Where(u => u.Nombre == request.Persona.Nombre
                                                                        && u.Identificacion == request.Persona.Identificacion)
                        .FirstOrDefaultAsync();

                    if (GetPersona != null)
                    {
                        error = "Ya existe una persona con los datos suministrados.";
                        respuesta.responseStatus = 404;
                        respuesta.responseData = new
                        {
                            error = error
                        };

                        return respuesta;
                    }


                    var persona = new Persona
                    {
                        Nombre = request.Persona.Nombre,
                        Genero = request.Persona.Genero,
                        Edad = request.Persona.Edad,
                        Identificacion = request.Persona.Identificacion,
                        Direccion = request.Persona.Direccion,
                        Telefono = request.Persona.Telefono
                    };

                    _context.Personas.Add(persona);

                    var nroRegUsario = await _context.SaveChangesAsync(); //commit a la transaccion

                    if (nroRegUsario > 0)
                    {
                        

                        respuesta.responseStatus = 200;
                        respuesta.responseData = new
                        {
                            PersonaId = Convert.ToString(persona.PersonaId)
                        };

                    }
                    else
                    {
                        error = "la persona no fue creada";
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

