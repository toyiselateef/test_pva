
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AccountBlockRequest{

        [JsonPropertyName("AccountNumber")]
    [Required]
        public string ab_bankaccount {get;set;}
        [JsonPropertyName("PndReason")]
        public  string pnd_reason { get;set;} 
        [JsonPropertyName("PhoneNumber")]
        public string phone_number {get;set;}

    }
 
