using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Service.Features.PersonaFeatures.Commands;
using Banco.Service.Features.PersonaFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BancoAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private const string get = "";
        private const string getById = "Persona/{Id}";
        private const string Create = "";
        private const string Update = "Persona/{Id}";
        private const string Delete = "Persona/{Id}";

        #region Private fields

        private readonly IMediator _mediator;

        #endregion Private fields

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public PersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion Constructor

        #region Implementation

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get All Persona", Description = "Returns All Persona", OperationId = "GetAllPersona")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(get)]
        public async Task<ActionResult> GetAllPersona()
        {
            var response = await _mediator.Send(new GetPersona { });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Get Persona By Id", Description = "Return Persona by Id", OperationId = "GetPersonaById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(getById)]
        public async Task<ActionResult> GetPersonaById(string Id)
        {
            string cacheName = Id;


            var response = await _mediator.Send(new GetPersonaById { IdPersona = int.Parse(Id), });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Create Persona", Description = "Create New Persona", OperationId = "CreatePersona")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPost(Create)]
        public async Task<ActionResult> CreatePersona(Persona persona)
        {
            var response = await _mediator.Send(new CreatePersonaCommand { Persona = persona, });
            return StatusCode((int)response.responseStatus, response);
        }


        [SwaggerOperation(Summary = "Update Persona", Description = "Update Persona By Id", OperationId = "UpdatePersona")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPut(Update)]
        public async Task<ActionResult> UpdatePersona(string Id, [FromBody] Persona Persona)
        {
            var response = await _mediator.Send(new UpdatePersonaCommand { IdPersona = int.Parse(Id), Persona = Persona });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "DeletePersona Persona", Description = "DeletePersona Persona By", OperationId = "DeletePersona")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpDelete(Delete)]
        public async Task<ActionResult> DeletePersona(string Id)
        {
            var response = await _mediator.Send(new DeletePersonaCommand { IdPersona = int.Parse(Id) });
            return StatusCode((int)response.responseStatus, response);
        }
        #endregion
    }
}
