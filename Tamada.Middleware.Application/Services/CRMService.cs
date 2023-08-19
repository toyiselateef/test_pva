using Microsoft.Extensions.Options; 
using Tamada.Middleware.Application.Interfaces; 

namespace Tamada.Middleware.Application.Services
{
    public class CRMService : ICRMService
    {
        private readonly IHttpFacade httpFacade;
        private readonly CRMResources CMRresources; 

        public CRMService(IHttpFacade httpFacade, IOptions<CRMResources> CMRresources)
        {
            this.httpFacade = httpFacade?? throw new ArgumentNullException(nameof(IHttpFacade));
            this.CMRresources = CMRresources.Value?? throw new ArgumentNullException(nameof(CRMResources));
             
        }
        public async Task<bool> LogCaseOnCrm(CrmCaseRequest request)
        {
            //var baseUrl = CMRresources.BaseUrl;

            //var resource = CMRresources.LogCaseResource?? "restapi/XrmWebApi/v1/createcase";

            //var req = new HttpRequest<CrmCaseRequest>
            //{
            //    BaseUrl = baseUrl,
            //    Resource = resource,
            //    Method = Domain.Entities.Enums.HttpMethod.Post, 
            //    RequestBody = request

            //};

            //var response = await httpFacade.SendRequest<CrmCaseRequest, object>(req);
            //if (response.IsSuccessful)
            //{
            //    return true;
            //}
            //return false;
            return new Random().Next(10) % 2 == 0;
        }
    }
}
