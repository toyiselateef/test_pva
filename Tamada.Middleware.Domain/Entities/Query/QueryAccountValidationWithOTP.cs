public class QueryAccountValidationWithOTP
{
    public string cust_ac_no { get; set; } = string.Empty;
    public string e_mail { get; set; } = string.Empty;
    public string mobile_number { get; set; } = string.Empty;
    public string? ac_stat_frozen { get; set; }
    public string? ac_stat_dormant { get; set; }
    public string? ac_stat_no_dr { get; set; }
    public string? ac_stat_block { get; set; }
}