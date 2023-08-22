using AutoMapper; 
using Microsoft.Extensions.Options; 
public class AccountService : IAccountService
{
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;
    private readonly IHttpFacade httpFacade;
    private readonly IOTPService oTPService;
    private readonly CRMResources CMRresources;

    public AccountService(IAccountRepository accountRepository,
     IMapper mapper,
     IHttpFacade httpFacade,
     IOptions<CRMResources> CMRresources,
     IOTPService oTPService)
    {
        this.accountRepository = accountRepository?? throw new ArgumentNullException(nameof(IMapper));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        this.httpFacade = httpFacade ?? throw new ArgumentNullException(nameof(IHttpFacade));
        this.oTPService = oTPService??throw new ArgumentNullException(nameof(IOTPService));
        this.CMRresources = CMRresources.Value ?? throw new ArgumentNullException(nameof(CRMResources));
    }
 

    public async Task<AccountEnquiry> AccountDetails(string accountNumber)
    { 

        if (string.IsNullOrEmpty(accountNumber))
        {
            throw new BadRequestException(UserMessages.GetAccountNoBadRequestUserFriendlyMessage());
        }
        AccountEnquiry acct_details_result = await accountRepository.FetchAccountDetails(accountNumber: accountNumber);

        if (acct_details_result == null)
        {
            throw new NotFoundException(UserMessages.GetAccountNotValidUserFriendlyMessage());
        }

        return  acct_details_result; 

    }

    public async Task<AccountStatus> AccountStatusEnquiry(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            throw new BadRequestException(UserMessages.GetAccountNoBadRequestUserFriendlyMessage());
        }
 
        var acct_status_result = await accountRepository.FetchAccountStatus(accountNumber: accountNumber);

        if (acct_status_result == null)
        {
            throw new BadRequestException(UserMessages.GetAccountNotValidUserFriendlyMessage());
        }

        return mapper.Map<AccountStatus>(acct_status_result); 
    }

   

    public async Task<AccountValidation> AccountValidation(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            throw new BadRequestException(UserMessages.GetAccountNoBadRequestUserFriendlyMessage());
        }
 
        var valid_acct_result = await accountRepository.FetchValidAccount(accountNumber: accountNumber);

        if (valid_acct_result== null)
        {
            throw new BadRequestException(UserMessages.GetAccountNotValidUserFriendlyMessage());
        }

        return  mapper.Map<AccountValidation>(valid_acct_result); 
    }
    public async Task<IEnumerable<string>> Accounts()
    {
       
        var valid_acct_result = await accountRepository.FetchAccounts();

        if (valid_acct_result== null)
        {
            throw new BadRequestException("no accounts found");
        }

        return  valid_acct_result; 
    }
    
    public async Task<AccountValidationWithOTP> Account_Validation_OTPGeneration(string accountNumber)
    {

        if (string.IsNullOrEmpty(accountNumber))
        {
            throw new BadRequestException(UserMessages.GetAccountNoBadRequestUserFriendlyMessage());
        }

        var valid_acct_result = await accountRepository.FetchValidAccountWithOTP(accountNumber: accountNumber);

        if (valid_acct_result == null)
        {
            throw new BadRequestException(UserMessages.GetAccountNotValidUserFriendlyMessage());
        }

        var res =  mapper.Map<AccountValidationWithOTP>(valid_acct_result);

        var otpResponse = await oTPService.GenerateAsync(res.mobile_number);

        if (!otpResponse.Generated)
        {
            throw new ServerErrorException("could not generate OTP at this time");
        }

        res.token = otpResponse.Token;

        return res;
    }

    public async Task<bool> BlockAccount(AccountBlockRequest request)
    {
        //bool result = false;
        //var baseUrl = CMRresources.BaseUrl?? "https://datacapturetool.azurewebsites.net";
        //var resource = CMRresources.BlockAccountResource ?? "restapi/XrmWebApi/v1/blockaccount";

        //var req = new HttpRequest<AccountBlockRequest>  
        //{
        //    BaseUrl = baseUrl,
        //    Resource = resource,
        //    Method = Tamada.Middleware.Domain.Entities.Enums.HttpMethod.Post, 
        //    RequestBody = request

        //}; 

        //var response = await httpFacade.SendRequest<AccountBlockRequest,CRMAccountBlockRes>(req);

        //result  = response.IsSuccessful && response.Data.ResponseCode == "00";

        //return result; 


        return new Random().Next(10) % 2 == 0;
       
    } 

}