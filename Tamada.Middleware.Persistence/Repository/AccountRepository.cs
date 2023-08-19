using System.Data;
using System.Text.Json.Serialization;
using Dapper;

public class AccountRepository : IAccountRepository
{

    private IEnumerable<QueryAccountValidation> accountValidation;
    private IEnumerable<QueryAccountStatus> accountStatus;
    private IEnumerable<AccountEnquiry> accountEnquiry;
    public AccountRepository()
    {
        GenerateRandoms();
    }

    public Task<QueryAccountValidation> FetchValidAccount(string accountNumber)
    {
        if (accountValidation == null)
        {
            accountValidation = new List<QueryAccountValidation>();
        }
        return Task.FromResult(accountValidation.FirstOrDefault(u => u.cust_ac_no == accountNumber)); 
    }
     

    public Task<QueryAccountStatus> FetchAccountStatus(string accountNumber)
    {
        if (accountStatus == null)
        {
            accountStatus = new List<QueryAccountStatus>();
        }
        return Task.FromResult(accountStatus.FirstOrDefault(u => u.cust_ac_no == accountNumber));
    }

    public  Task<AccountEnquiry> FetchAccountDetails(string accountNumber)
    {
    if (accountEnquiry == null)
    {
            accountEnquiry = new List<AccountEnquiry>();
    }
    return Task.FromResult(accountEnquiry.FirstOrDefault(u => u.AccNo == accountNumber));
    }


    public void GenerateRandoms()
    {
        List<QueryAccountValidation> records = new List<QueryAccountValidation>();
        List<QueryAccountStatus> recordstatus = new List<QueryAccountStatus>();
        List<AccountEnquiry> recordenquiry = new List<AccountEnquiry>();

        for (int i = 4; i <= 30; i++)
        {
        var accno =  $"0934848555{i:00}";
        var frozen = i % 2 == 0 ? "Y" : "N";
        var dormant = i % 3 == 0 ? "Y" : "N";
        var dr = i % 4 == 0 ? "Y" : "N";
        var block = i % 5 == 0 ? "Y" : "N";

        records.Add(new QueryAccountValidation
            {
                cust_ac_no =  accno,
                ac_stat_frozen = frozen,
                ac_stat_dormant = dormant,
                ac_stat_no_dr = dr,
                ac_stat_block = block
            });
        recordstatus.Add(new QueryAccountStatus
        {
            cust_ac_no = accno,
                ac_stat_no_dr = dr,
                description = $"Description {i}",
                record_stat = i % 3 == 0,
                ac_stat_block = block,
                ac_stat_dormant = dormant,
                ac_stat_frozen = frozen,
                ACC_STATUS = $"Status {i}"
        });

        recordenquiry.Add(new AccountEnquiry()
        { 
            e_mail = $"{accno}@gmail.com",
            mobile_number = accno,
        });
    }


         accountValidation = records;
        accountStatus = recordstatus; ;
        accountEnquiry = recordenquiry;
    }

}