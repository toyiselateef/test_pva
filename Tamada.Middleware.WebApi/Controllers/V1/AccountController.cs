using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

//[Authorize("ApiKeyPolicy")]
[Authorize]
public class AccountController : BaseController
{
    private readonly IAccountService accountService;
    private readonly ILogger<AccountController> logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        this.accountService = accountService;
        this.logger = logger;
    }


    [HttpGet("AllAccounts")]
    #region <swagger description>
    [SwaggerOperation(Summary = "AllAccounts", Description = "get all accounts.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<AccountValidation>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> AllAccounts()
    {
        var response = await accountService.Accounts();

          return Ok(new ApiResponse<IEnumerable<string>>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }
     [HttpPost("Validate")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Validate Account", Description = "Validate an account is an access account and its valid.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<AccountValidation>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> Validate([FromBody] AccountNumberRequest req)
    {
        AccountValidation response = await accountService.AccountValidation(req.accNo);

        logger.LogInformation($"Validate account for {req.accNo?.Substring(0, 6)} with response : {JsonSerializer.Serialize(response)}");

        return Ok(new ApiResponse<AccountValidation>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost("Status")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Get Account status", Description = "Get the status of an account using account number.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<AccountStatus>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> Status([FromBody] AccountNumberRequest req)
    {
        AccountStatus response = await accountService.AccountStatusEnquiry(req.accNo);

        logger.LogInformation($"Account status enquiry for {req.accNo?.Substring(0, 6)} with response : {JsonSerializer.Serialize(response)}");

        return Ok(new ApiResponse<AccountStatus>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost("Details")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Account Details fetch", Description = "Fetch an account details including email and mobile no using account number.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<AccountEnquiry>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> Details([FromBody] AccountNumberRequest req)
    {
        AccountEnquiry response = await accountService.AccountDetails(req.accNo);

        logger.LogInformation($"Account Detaisl enquiry for account : {req.accNo?.Substring(0, 6)} with response : {JsonSerializer.Serialize(response)}");

        return Ok(new ApiResponse<AccountEnquiry>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }

    
    [HttpPost("Block")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Block Account", Description = "Block an existing account.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<bool>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<IActionResult> BlockAccount([FromBody] AccountBlockRequest req)
    {
        var response = await accountService.BlockAccount(req);
        logger.LogInformation($"Block account for account : {req.ab_bankaccount?.Substring(0, 6)} with response : {JsonSerializer.Serialize(response)}");

        return Ok(new ApiResponse<bool>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }
}