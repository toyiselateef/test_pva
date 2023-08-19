
using System.Net.Mail;

public class Email
    {
        
        public string? Subject { get; set; }
        public string? Body { get; set; } 

        // [Required(ErrorMessage = "The email address is required")]
         public string? FromAddresses { get; set; }


       // [Required(ErrorMessage = "The email address is required")]
        public string ToAddresses { get; set; } = string.Empty;

        public string? AppName { get; set; }
        public bool HasAttachment { get; set; }
        public string? BccAddresses { get; set; }
        public string? CCAddresses { get; set; }
        public List<EmailAttachment> EmailAttachments { get; set; }
        public DateTime DateSubmitted { get; set; } =  DateTime.Now;
        public DateTime Datesent { get; set; } 
        public string Status { get; set; }
   
}


    public class EmailAttachmentDetails
    {
        public SizeObj Size { get; set; }
        public string? FileName { get; set; }
        public Attachment Attachment { get; set; }



    }
     public class EmailAttachment
    {
        
        public string? FileName { get; set; }
        public Byte[] Data { get; set; }
        public string? FileType { get; set; }
}

    public class SizeObj
    {
        public double size { get; set; }
        public string? si { get; set; }
    }
