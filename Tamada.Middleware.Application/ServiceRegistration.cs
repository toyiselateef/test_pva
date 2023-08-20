
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tamada.Middleware.Application.Interfaces;
using Tamada.Middleware.Application.Services;

public static class ServiceRegistration
{

    public static void AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICRMService, CRMService>();
        services.AddScoped<IAppService, AppService>();
        services.AddScoped<ISMSService, SMSService>();
        services.AddScoped<IOTPService, OTPService>();
        services.AddScoped<IEmailService,EmailService>();
        services.AddScoped<IHttpFacade,HttpFacade>();
        services.AddSingleton<ExceptionHandlingMiddleware>();
      
    }
}
