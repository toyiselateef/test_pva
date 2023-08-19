using System.Net.Mail;
using System.Net.Mime;

public static class EmailHelper {
     public static void AddToAddresses(this MailMessage mailMessage, string to_addresses)
      {
         var to_addresses_array =  to_addresses.Split(';');
         foreach (string to_address in to_addresses_array)
         {
                   
            var valid_email = ValidateEmail(to_address.Trim());
            if (valid_email)
            {
                mailMessage.To.Add(new MailAddress(to_address));
            }
         } 

      }

      public static void PrepareAttachments(this MailMessage mailMessage, List<EmailAttachment> emailAttachments)
     {
            
           foreach (EmailAttachment emailAttachment in emailAttachments)
            {
                if(emailAttachment.Data?.Length < 1 || string.IsNullOrEmpty(emailAttachment.FileName))
                {
                    continue;
                }

                var attachment = GetAttachment(emailAttachment.Data, emailAttachment.FileName??"attachment", emailAttachment.FileType);

                if (attachment.Size.size > 10000.0)
                {
                   continue;
                }
                else
                {
                    mailMessage.Attachments.Add(attachment.Attachment); 
                }
            }
      }



       private static EmailAttachmentDetails GetAttachment(Byte[] data, string filename, string FileTypeExt)
        {
          
            filename = $"{filename}.{FileTypeExt}";
            MemoryStream memoryStream = new MemoryStream(data);
            memoryStream.Position = 0;
            Attachment attachment = new Attachment(memoryStream, filename, MediaTypeNames.Application.Pdf);
            ContentDisposition disposition = attachment.ContentDisposition;
            disposition.FileName = filename;
            disposition.Size = memoryStream.Length;
            disposition.DispositionType = DispositionTypeNames.Attachment;
            return new EmailAttachmentDetails { Size = BytesSize(data.LongLength), FileName = filename, Attachment = attachment };
        }

        private static SizeObj BytesSize(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return new SizeObj { size = 0, si = suf[0] };
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            var s = (Math.Sign(byteCount) * num);
            return new SizeObj { size = s, si = (suf[place]) };
        }



        private static bool ValidateEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

 
}



