using System.Data;

public class AccountRepository : IAccountRepository
{
    private readonly GeneratorClass _generator;

    public AccountRepository( GeneratorClass generator)
    {
        
        _generator = generator;
    }

    public Task<QueryAccountValidation> FetchValidAccount(string accountNumber)
    {
        if (_generator.accountValidation == null)
        {
            _generator.accountValidation = new List<QueryAccountValidation>();
        }
        return Task.FromResult(_generator.accountValidation.FirstOrDefault(u => u.cust_ac_no == accountNumber)); 
    }
    public Task<QueryAccountValidationWithOTP> FetchValidAccountWithOTP(string accountNumber)
    {
        if (_generator.accountValidationWOTP == null)
        {
            _generator.accountValidationWOTP = new List<QueryAccountValidationWithOTP>();
        }
        return Task.FromResult(_generator.accountValidationWOTP.FirstOrDefault(u => u.cust_ac_no == accountNumber)); 
    }
     

    public Task<QueryAccountStatus> FetchAccountStatus(string accountNumber)
    {
        if (_generator.accountStatus == null)
        {
            _generator.accountStatus = new List<QueryAccountStatus>();
        }
        return Task.FromResult(_generator.accountStatus.FirstOrDefault(u => u.cust_ac_no == accountNumber));
    }

    public  Task<AccountEnquiry> FetchAccountDetails(string accountNumber)
    {
    if (_generator.accountEnquiry == null)
    {
            _generator.accountEnquiry = new List<AccountEnquiry>();
    }
    return Task.FromResult(_generator.accountEnquiry.FirstOrDefault(u => u.AccNo == accountNumber));
    }
    public  Task<IEnumerable <string>> FetchAccounts()
    {
    if (_generator.accountValidation == null)
    {
            _generator.accountValidation = new List<QueryAccountValidation>();
    }
    return Task.FromResult(_generator.accountValidation.Select(x=> $"{x.cust_ac_no} ==> isValiid: {!(x.ac_stat_no_dr == "Y" || x.ac_stat_dormant == "Y" || x.ac_stat_frozen == "Y" || x.ac_stat_block == "Y")}"));

    }




}