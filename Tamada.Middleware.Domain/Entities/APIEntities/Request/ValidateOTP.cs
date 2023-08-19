using System.ComponentModel.DataAnnotations;

 
    public class ValidateOTP
    {
        [Required(ErrorMessage = "phone Number is required.")]
        // [StringLength(11, ErrorMessage = "phone Number cannot exceed 11 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Token is required.")]
        // [StringLength(11, ErrorMessage = "Token cannot exceed 11 characters.")]
        public string Token { get; set; } = string.Empty;
    }
 
