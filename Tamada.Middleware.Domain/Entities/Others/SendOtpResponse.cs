
public class SendOtpResponse
{
    public bool Generated { get; set; }
    public string? phoneNumber { get; set; }
    public DateTime Time { get; set; }
    public string Token { get; set; }
} 