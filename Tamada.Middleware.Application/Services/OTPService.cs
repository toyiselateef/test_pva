using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtpNet; 

public class OTPService : IOTPService
{

    private readonly OtpSettings otpOptions;
    private readonly ILogger<OTPService> logger;
    private readonly IMemoryCache cache;
    private readonly ISMSService smsService;
    private readonly IEmailService emailService;
    private readonly  CacheSettings cacheOptions;
    private readonly  string AppName = "TAMADA";

    public OTPService(IOptions<OtpSettings> otpOptions,
        ILogger<OTPService> logger, IMemoryCache cache,
        IOptions<CacheSettings> cacheOptions, ISMSService smsService,
        IEmailService emailService)
    {
        this.otpOptions = otpOptions.Value ?? throw new ArgumentNullException(nameof(OtpSettings));
        this.logger = logger ?? throw new ArgumentNullException(nameof(ILogger<OTPService>));
        this.cache = cache ?? throw new ArgumentNullException(nameof(IMemoryCache));
        this.smsService = smsService ?? throw new ArgumentNullException(nameof(ISMSService));
        this.emailService = emailService ?? throw new ArgumentNullException(nameof(IEmailService));
        this.cacheOptions = cacheOptions.Value ?? throw new ArgumentNullException(nameof(CacheSettings));
    }

    public async Task<SendOtpResponse> GenerateAsync(string phoneNumber, string? accessKey = null)
    {

        SendOtpResponse otpResponse = new SendOtpResponse() { phoneNumber = phoneNumber};
        byte[] accessKeyBytes;
       
        if (string.IsNullOrWhiteSpace(accessKey))
        {
            accessKeyBytes = KeyGeneration.GenerateRandomKey(32);
            accessKey = Base32Encoding.ToString(accessKeyBytes);
        }
        else
        {
            accessKeyBytes = Base32Encoding.ToBytes(accessKey);
        } 

         
      

        var totp = new Totp(accessKeyBytes, mode: OtpHashMode.Sha512, totpSize: otpOptions.OtpSize,
            step: otpOptions.DurationInSeconds);
        var token = totp.ComputeTotp();

        var message = string.Format(otpOptions.SmsTemplate, AppName, token);
       // if (await smsService.SendAsync(message, phoneNumber, AppName))
        //var email_response = await emailService.SendAsync(new EmailRequest{ 
        //                                            ToAddresses = "",
        //                                            Body = message + "<br><br><p> expires in <b>10 minutes<b> <br><br> Use immediately<p>",
        //                                            Subject = "TAMADA - OTP" 
                                                     
        //                                          });
        //if (email_response.Sent)
        //{
           
             
       
        cache.Set(phoneNumber, accessKey,
            TimeSpan.FromHours(cacheOptions.ExpirationInHours));
        logger.LogInformation($"OTP sent for {phoneNumber.Substring(0, 6)}");
        otpResponse.Token = token;
        otpResponse.Generated = true;

      // }
        return otpResponse;
    } 

    public  Task<bool> ValidateAsync(string? phoneNumber, string? token)
    {
        if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(token))
        {
            throw new BadRequestException("No Token Supplied");
        }

        if (!cache.TryGetValue(phoneNumber, out string accessKey))
        {
            throw new NotFoundException("Token Supplied not Found or Valid");
        }

        var accessKeyBytes = Base32Encoding.ToBytes(accessKey);
        var totp = new Totp(accessKeyBytes, mode: OtpHashMode.Sha512, totpSize: otpOptions.OtpSize,
            step: otpOptions.DurationInSeconds);

        bool isValid = totp.VerifyTotp(token, out _);
        return Task.FromResult(isValid);
    }
     
} 

