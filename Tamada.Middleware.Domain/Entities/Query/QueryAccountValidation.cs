using System.Text.Json.Serialization;

public class QueryAccountValidation {  
  public string cust_ac_no { get; set; } = string.Empty; 
  public string? ac_stat_frozen { get; set; } 
  public string? ac_stat_dormant { get; set; } 
  public string? ac_stat_no_dr { get; set; } 
  public string? ac_stat_block { get; set; }
}