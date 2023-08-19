using System.Text.Json.Serialization;

public class AccountEnquiry{
    // public int AccountNumber { get; set; }

    [JsonPropertyName("AccountNo")]
    public string AccNo { get; set; } = string.Empty;
    [JsonPropertyName("Email")]
public string e_mail { get; set; } = string.Empty;
[JsonPropertyName("PhoneNumber")]
public string mobile_number { get; set; } = string.Empty;
}