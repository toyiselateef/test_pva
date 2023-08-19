using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AccountNumberRequest{
 
    [Required(ErrorMessage = "account Number is required.")]
    [StringLength(11, ErrorMessage = "Account Number cannot exceed 11 characters.")]
    [JsonPropertyName("accountNumber")]
    public string accNo { get; set; } = string.Empty;

   
} 