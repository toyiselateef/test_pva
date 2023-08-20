 
    public class GeneratorClass
    {
    public GeneratorClass()
    {
        GenerateRandoms();
    }
    public IEnumerable<QueryAccountValidation> accountValidation;
    public IEnumerable<QueryAccountStatus> accountStatus;
    public IEnumerable<AccountEnquiry> accountEnquiry;
    private void GenerateRandoms()
    {
        List<QueryAccountValidation> records = new List<QueryAccountValidation>();
        List<QueryAccountStatus> recordstatus = new List<QueryAccountStatus>();
        List<AccountEnquiry> recordenquiry = new List<AccountEnquiry>();

        for (int i = 4; i <= 30; i++)
        {
            var accno = $"09348485{i:00}";
            var frozen = i % 2 == 0 ? "Y" : "N";
            var dormant = i % 3 == 0 ? "Y" : "N";
            var dr = i % 4 == 0 ? "Y" : "N";
            var block = i % 5 == 0 ? "Y" : "N";

            records.Add(new QueryAccountValidation
            {
                cust_ac_no = accno,
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

