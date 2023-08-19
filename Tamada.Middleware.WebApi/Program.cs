using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });


builder.Services.AddControllers();
builder.Services.AddMemoryCache();

#region <Auth>
//builder.Services.AddAuthentication("ApiKeyAuthentication")
//           .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyAuthentication", null);

builder.Services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiKeyPolicy", policy =>
            {
                policy.AuthenticationSchemes.Add("ApiKeyAuthentication");
                policy.RequireAuthenticatedUser();
            });
        });
#endregion
#region <CORS>
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowPowerAutomate",
            builder =>
            {
                builder.WithOrigins("https://*.microsoft.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
#endregion
#region <rateLimitservices>
//builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
//builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
//builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
//builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
#endregion
#region <versioning>
//builder.Services.AddApiVersioning(options =>
//{
//    options.ReportApiVersions = true;
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    options.AssumeDefaultVersionWhenUnspecified = true;
//});
#endregion

#region <register config models>
builder.Services.Configure<SMTPConfig>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.Configure<SMSResources>(builder.Configuration.GetSection("SMSResource"));
builder.Services.Configure<CRMResources>(builder.Configuration.GetSection("SMSResource"));
builder.Services.Configure<OtpSettings>(builder.Configuration.GetSection("OtpSettings"));
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

#endregion

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tamada.Middleware",
                    Version = "v1",
                    Description = "Tamada Middleware",
                });

                c.EnableAnnotations();

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic authentication header"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                      {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });
            });

//c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
//                {
//                    Type = SecuritySchemeType.ApiKey,
//                    In = ParameterLocation.Header,
//                    Name = "X-API-Key", // This will be the name of the API key parameter in the header
//                    Description = "API Key for authorization",
//                });


//                c.AddSecurityRequirement(new OpenApiSecurityRequirement
//                         {
//                            {
//                              new OpenApiSecurityScheme
//                              {
//                                Reference = new OpenApiReference
//                                {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "ApiKey"
//                        }
//                    },
//                    new string[] {}
//                }
//            });

//             //   Include XML comments(optional)
//             //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//             //   var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//             //   c.IncludeXmlComments(xmlPath);
//            });


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tamada Middleware V1"));
//}

//if(!app.Environment.IsDevelopment()){
//    app.UseCors("AllowPowerAutomate");
//}
#region <CSP>
app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
        await next();
    });
#endregion 
app.UseHttpsRedirection();

 app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
