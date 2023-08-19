 using Microsoft.AspNetCore.Mvc;
 
 //[ApiVersion("1.0")]
 //[Route("api/v{version:apiVersion}/[controller]")]
 [Route("[controller]")]
 [ApiController]
 [CustomValidation]
    [ProducesErrorResponseType(typeof(ErrorResponse))] 
    public class BaseController : ControllerBase
    {

    }