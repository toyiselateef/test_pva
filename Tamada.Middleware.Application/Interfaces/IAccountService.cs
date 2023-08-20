 
public interface IAccountService {

    Task<AccountValidation> AccountValidation(string AccountNumber);
    Task<AccountStatus> AccountStatusEnquiry(string AccountNumber);
    Task<AccountEnquiry> AccountDetails(string AccountNumber);
    Task<IEnumerable<string>> Accounts();
    Task<bool> BlockAccount(AccountBlockRequest request);
}