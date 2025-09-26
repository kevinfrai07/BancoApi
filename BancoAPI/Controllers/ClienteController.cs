using Banco.Domain.Entity;
using Banco.Service.Features.ClienteFeatures.Commands;
using Banco.Service.Features.ClienteFeatures.Queries;
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
    public class ClienteController : ControllerBase
    {
        private const string get = "";
        private const string getById = "{Id}";
        private const string Create = "";
        private const string Update = "{Id}";
        private const string Delete = "{Id}";

        #region Private fields

        private readonly IMediator _mediator;

        #endregion Private fields

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion Constructor

        #region Implementation

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get All Cliente", Description = "Returns All Cliente", OperationId = "GetAllCliente")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(get)]
        public async Task<ActionResult> GetAllCliente()
        {
            var response = await _mediator.Send(new GetCliente { });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Get Cliente By Id", Description = "Return Cliente by Id", OperationId = "GetClienteById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(getById)]
        public async Task<ActionResult> GetClienteById(string Id)
        {
            string cacheName = Id;


            var response = await _mediator.Send(new GetClienteById { IdCliente = int.Parse(Id), });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Create Cliente", Description = "Create New Cliente", OperationId = "CreateCliente")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPost(Create)]
        public async Task<ActionResult> CreateCliente(Cliente Cliente)
        {
            var response = await _mediator.Send(new CreateClienteCommand { Cliente = Cliente, });
            return StatusCode((int)response.responseStatus, response);
        }


        [SwaggerOperation(Summary = "Update Cliente", Description = "Update Cliente By Id", OperationId = "UpdateCliente")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPut(Update)]
        public async Task<ActionResult> UpdateCliente(string Id, [FromBody] Cliente Cliente)
        {
            var response = await _mediator.Send(new UpdateClienteCommand { IdCliente = int.Parse(Id), Cliente = Cliente });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "DeleteCliente Cliente", Description = "DeleteCliente Cliente By", OperationId = "DeleteCliente")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpDelete(Delete)]
        public async Task<ActionResult> DeleteCliente(string Id)
        {
            var response = await _mediator.Send(new DeleteClienteCommand { IdCliente = int.Parse(Id) });
            return StatusCode((int)response.responseStatus, response);
        }
        #endregion
    }
}
