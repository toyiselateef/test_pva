 
    public interface IEmailService
    {
        //Task<ApiResponse<string>> SendMail(EmailRequest request);
        Task<EmailResponse> SendAsync(EmailRequest email);
    }

