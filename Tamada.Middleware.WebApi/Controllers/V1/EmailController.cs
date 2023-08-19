// using Microsoft.AspNetCore.Mvc;
// using Tamada.Middleware.Domain.Entities.APIEntities.Request;
// using Tamada.Middleware.Domain.Interface.Services;

// namespace Tamada.Middleware.WebApi.Controllers.V1
// {
//     public class EmailController : BaseController
//     {
//         private readonly IEmailService _emailService;

//         public EmailController(IEmailService emailService)
//         {
//             _emailService = emailService;
//         }

//         [HttpPost]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(200)]
//         public async Task<ActionResult<bool>> SendMail(EmailRequest request)
//         {
//             var response = await _emailService.SendMail(request);

//             return Ok(response);
//         }
//     }
// }
