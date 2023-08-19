using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
    public class SMSService : ISMSService
    {
        private readonly ILogger<SMSService> logger;
        private readonly SMSResources SMSresource;
        private readonly IHttpFacade httpFacade;
        private readonly IMapper mapper;

        public SMSService(ILogger<SMSService> logger, IOptions<SMSResources> SMSresource, IHttpFacade httpFacade, IMapper mapper)
        {
            this.logger = logger;
            this.SMSresource = SMSresource.Value;
            this.httpFacade = httpFacade;
            this.mapper = mapper;
        }

        public async Task<bool> SendAsync(SMSRequest SMSRequest)
        {
            SMS smsDetails = mapper.Map<SMS>(SMSRequest);
            
            smsDetails.appId = "ussd";
            smsDetails.appReference = "xyz-123";

            var sub_key = !string.IsNullOrEmpty(SMSresource.SubscriptionKey) ? SMSresource.SubscriptionKey : Environment.GetEnvironmentVariable("SandBox_SubscriptionKey");
            var result = false;
            var httpRequest = new HttpRequest<SMS>
            {
               BaseUrl = SMSresource.BaseUrl,
               Resource = SMSresource.SendEndpoint,
               Method = Tamada.Middleware.Domain.Entities.Enums.HttpMethod.Post,
               Headers = new Dictionary<string, string>
               {
                   { "Subscription-Key", sub_key },
                   { "Cache-Control", "no-cache" }
               },
               //mocked up data
               RequestBody = smsDetails
            };
       

            var response = await httpFacade.SendRequest<SMS,SMSAPIRes>(httpRequest);

            if(response.IsSuccessful && response.Data?.status == "success") { 
            
              result = true;
            }

            return result;

        }
    }

