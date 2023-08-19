
using System.Text.Json.Serialization;

public class SMSRequest{
        [JsonPropertyName("PhoneNumber")] 
    public string phoneNumber { get; set; } 
     [JsonPropertyName("Message")]
    public string message { get; set; }
     [JsonPropertyName("Priority")]
    public bool priority { get; set; }
     [JsonPropertyName("DebitAccountNumber")]
    public string debitAccount { get; set; }
     [JsonPropertyName("Topic")]
    public string topic { get; set; }
    }
    
    