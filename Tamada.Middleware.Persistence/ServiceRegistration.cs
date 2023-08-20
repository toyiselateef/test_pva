using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration{

 public static void AddPersistanceServices(this IServiceCollection  services, IConfiguration Configuration)
        {

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAppRepository, AppRepository>();
        //services.AddSingleton<IDBConnectionFactory, DBConnectionFactory>(provider => 
        //        new DBConnectionFactory(Environment.GetEnvironmentVariable("SqlServerConnection"),
        //                                Environment.GetEnvironmentVariable("OracleServerConnection")));

        //services.AddSingleton<IDBConnectionFactory, DBConnectionFactory>(provider => 
        //        new DBConnectionFactory(Configuration.GetConnectionString("SqlServerConnection"),
        //                                Configuration.GetConnectionString("OracleServerConnection")));
        services.AddSingleton<GeneratorClass>();


    }
}