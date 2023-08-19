
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

//[Authorize("ApiKeyPolicy")]
[Authorize]
public class AppController : BaseController {
    private readonly IAppService appService;
    
    public AppController(IAppService appService)
    {
        this.appService = appService;
       
    }


    [HttpGet("Add/{appId}")]
    #region <swagger description>
    [SwaggerOperation(Summary = "GetApp", Description = "NB:still in progress...")]
    [SwaggerResponse(StatusCodes.Status200OK, "request successful", typeof(ApiResponse<bool>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public ActionResult GetApp([FromQuery] string appId)
        {
            //var response = await appService.ValidateAccountNumber(accountNumber);

            return Ok(new ApiResponse<bool>
            {
            Success = true,
            Message = "Request successful.",
            Data = true,
            StatusCode = StatusCodes.Status200OK
        });
        } 
}