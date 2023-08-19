
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tamada.Middleware.Application.Interfaces;

namespace Tamada.Middleware.WebApi.Controllers.V1
{
    //[Authorize("ApiKeyPolicy")]
    [Authorize]
    public class CRMController : BaseController
    {
        private readonly ICRMService crmService;

        public CRMController(ICRMService crmService)
        {
            this.crmService = crmService;
        }

        [HttpPost("logcase")]
        #region <swagger description>
        [SwaggerOperation(Summary = "Log Case", Description = "Log case on CRM.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<bool>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
        #endregion
        public async Task<IActionResult> LogOnCRM([FromBody]CrmCaseRequest request)
        {
            var response = await crmService.LogCaseOnCrm(request);
            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Request successful.",
                Data = response,
                StatusCode = StatusCodes.Status200OK
            });
        }
    }
}
