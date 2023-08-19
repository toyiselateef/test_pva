
    public interface IOTPService
    { 
            Task<SendOtpResponse> GenerateAsync(string phoneNumber, string? accessKey = null);
            Task<bool> ValidateAsync(string? PhoneNumber, string? token);
        
    }

