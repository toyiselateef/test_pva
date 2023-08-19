using System.ComponentModel.DataAnnotations; 

 
    public class EmailRequest
    {
         [Required(ErrorMessage = "The email address is required")]
        public string? Subject { get; set; }

         [Required(ErrorMessage = "The email address is required")]
        public string? Body { get; set; }


        [Required(ErrorMessage = "The email address is required")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        public string? FromAddresses { get; set; }


        [Required(ErrorMessage = "The email address is required")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        public string? ToAddresses { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        public string? TopicName { get; set; }
        public bool HasAttachment { get; set; }
        public string? BccAddresses { get; set; }
        public string? CCAddresses { get; set; }
        public List<EmailAttachment> EmailAttachments { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime Datesent { get; set; } 
    }
