public interface IAccountRepository{

 Task<QueryAccountValidation> FetchValidAccount(string accountNumber);
 Task<AccountEnquiry> FetchAccountDetails(string accountNumber);
 Task<QueryAccountStatus> FetchAccountStatus(string accountNumber);
 Task<IEnumerable<string>> FetchAccounts();
}