using Banco.Domain.DTO;
using Banco.Domain.Entity;
using Banco.Service.Features.CuentaFeatures.Commands;
using Banco.Service.Features.CuentaFeatures.Queries;
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
    public class CuentaController : ControllerBase
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
        public CuentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion Constructor

        #region Implementation

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get All Cuenta", Description = "Returns All Cuenta", OperationId = "GetAllCuenta")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(get)]
        public async Task<ActionResult> GetAllCuenta()
        {
            var response = await _mediator.Send(new GetCuenta { });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Get Cuenta By Id", Description = "Return Cuenta by Id", OperationId = "GetCuentaById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(getById)]
        public async Task<ActionResult> GetCuentaById(string Id)
        {
            var response = await _mediator.Send(new GetCuentaById { NumeroCuenta = Id, });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Create Cuenta", Description = "Create New Cuenta", OperationId = "CreateCuenta")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPost(Create)]
        public async Task<ActionResult> CreateCuenta(Cuenta Cuenta)
        {
            var response = await _mediator.Send(new CreateCuentaCommand { Cuenta = Cuenta, });
            return StatusCode((int)response.responseStatus, response);
        }


        [SwaggerOperation(Summary = "Update Cuenta", Description = "Update Cuenta By Id", OperationId = "UpdateCuenta")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPut(Update)]
        public async Task<ActionResult> UpdateCuenta(string Id, [FromBody] Cuenta Cuenta)
        {
            var response = await _mediator.Send(new UpdateCuentaCommand { NumeroCuenta = Id, Cuenta = Cuenta });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "DeleteCuenta Cuenta", Description = "DeleteCuenta Cuenta By", OperationId = "DeleteCuenta")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpDelete(Delete)]
        public async Task<ActionResult> DeleteCuenta(string Id)
        {
            var response = await _mediator.Send(new DeleteCuentaCommand { NumeroCuenta = Id });
            return StatusCode((int)response.responseStatus, response);
        }
        #endregion
    }
}
