public class AppService : IAppService
{ 
    private readonly IAppRepository appRepository;

    public AppService(IAppRepository appRepository)
    { 
        this.appRepository = appRepository;
    }
    public async Task<bool> ValidateAccountNumber(string AccountNumber)
    {
        if (string.IsNullOrEmpty(AccountNumber))
        {
            throw new BadRequestException("Invalid account number provided.");
        }

       // call flexcube aor esb service to retrieve account number
        var accountDetails = await appRepository.FetchAccountByAccountNumber(); 
        
        if (string.IsNullOrEmpty(accountDetails))
        {
            throw new BadRequestException(UserMessages.GetAccountNotValidUserFriendlyMessage());// we can get this message from static messages stored
        }

        return  true;
    }
}