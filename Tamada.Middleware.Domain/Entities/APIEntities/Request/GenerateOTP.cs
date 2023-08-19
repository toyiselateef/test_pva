

using System.ComponentModel.DataAnnotations; 

 
    public class GenerateOTP
    {
        [Required(ErrorMessage = "phone Number is required.")]
         [StringLength(11, ErrorMessage = "phone Number cannot exceed 11 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
 
