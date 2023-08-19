using System.ComponentModel;
using System.Text.Json.Serialization;

public class QueryAccountStatus 
{
    

     
    public string? cust_ac_no { get; set; }

     
    public string? ac_stat_no_dr{get;set;}


     
    public string description { get; set; } = string.Empty;

   
    public bool record_stat { get; set; }


     
    public string? ac_stat_block{get;set;}

   
    public string? ac_stat_dormant{get;set;}

     
    public string? ac_stat_frozen{get;set;}

      public string ACC_STATUS { get; set; } = string.Empty;
 
    
}
