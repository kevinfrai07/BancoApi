using Banco.Domain.Entity;
using Banco.Service.Features.MovimientoFeatures.Commands;
using Banco.Service.Features.MovimientosFeatures.Queries;
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
    public class MovimientosController : ControllerBase
    {
        private const string get = "";
        private const string getByCliente = "reporte/{Id}";
        private const string Create = "";

        #region Private fields

        private readonly IMediator _mediator;

        #endregion Private fields

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public MovimientosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion Constructor

        #region Implementation

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get All Movimientos", Description = "Returns All Movimientos", OperationId = "GetAllMovimientos")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(get)]
        public async Task<ActionResult> GetAllMovimientos()
        {
            var response = await _mediator.Send(new GetMovimientos { });
            return StatusCode((int)response.responseStatus, response);
        }

        [SwaggerOperation(Summary = "Get Movimientos By Id", Description = "Return Movimientos by Id", OperationId = "GetMovimientosById")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpGet(getByCliente)]
        public async Task<ActionResult> GetMovimientosById(int Id)
        {
            var response = await _mediator.Send(new GetMovimientosByCliente { clienteId = Id, });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Create Movimientos", Description = "Create New Movimientos", OperationId = "CreateMovimientos")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(IActionResult))]
        [HttpPost(Create)]
        public async Task<ActionResult> CreateMovimientos(Movimiento Movimientos)
        {
            var response = await _mediator.Send(new CreateMovimientosCommand { Movimiento = Movimientos, });
            return StatusCode((int)response.responseStatus, response);
        }


        #endregion
    }
}
