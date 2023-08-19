using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

//[Authorize("ApiKeyPolicy")]
[Authorize]
public class UtilityController : BaseController
{
    private readonly ILogger<UtilityController> logger;
    private readonly IEmailService EmailService;
    private readonly ISMSService SMSService;
    private readonly IOTPService OTPService;

    public UtilityController(ILogger<UtilityController> logger, IEmailService EmailService, ISMSService SMSService, IOTPService OTPService)
    {
        this.logger = logger;
        this.EmailService = EmailService;
        this.SMSService = SMSService;
        this.OTPService = OTPService;

    }

    [HttpPost("Email/Send")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Send Email", Description = "Send a Tamada Email.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<bool>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(EmailResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> SendMail(EmailRequest req)
    {

        EmailResponse response = await EmailService.SendAsync(req);

        return Ok(new ApiResponse<EmailResponse>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost("SMS/Send")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Send SMS", Description = "Send a Tamada SMS.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<bool>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(EmailResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> SendSMS(SMSRequest req)
    {
        var response = await SMSService.SendAsync(req);

        return Ok(new ApiResponse<bool>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost("OTP/Validate")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Validate OTP", Description = "Validate Customer OTP in Tamada.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<bool>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(EmailResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> ValidateOTP(ValidateOTP req)
    {

        bool response = await OTPService.ValidateAsync(req.PhoneNumber, req.Token);

        logger.LogInformation($"Validated OTP for {req.PhoneNumber?.Substring(0, 6)} with response : {response}");

        return Ok(new ApiResponse<bool>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });

    }

    [HttpPost("OTP/Generate")]
    #region <swagger description>
    [SwaggerOperation(Summary = "Generate OTP", Description = "Generate OTP for Customers in Tamada.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ApiResponse<bool>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "server error", typeof(EmailResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "UnAuthorized request", typeof(ErrorResponse))]
    #endregion
    public async Task<ActionResult> GenerateOTP(GenerateOTP req)
    {

        SendOtpResponse response = await OTPService.GenerateAsync(req.PhoneNumber);

        logger.LogInformation($"Generated OTP for {req.PhoneNumber?.Substring(0, 6)} with response : {JsonSerializer.Serialize(response)}");



        return Ok(new ApiResponse<SendOtpResponse>
        {
            Success = true,
            Message = "Request successful.",
            Data = response,
            StatusCode = StatusCodes.Status200OK
        });
    }
}

