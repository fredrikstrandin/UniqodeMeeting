using GlobalExceptionHandler.WebApi;
using HeroesUtils.BuilderExtentions;
using HeroesWeb.Hub;
using HeroesWeb.Models;
using HeroMemoryRepository.DependencyInjection;
using HeroMongoDBRepository.DependencyInjection;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;

namespace HeroesWeb
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly IHostingEnvironment _environment;

        public Startup(
            ILogger<Startup> logger,
            IHostingEnvironment environment,
            IConfiguration configuration)
        {
            _logger = logger;
            _environment = environment;

            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true);


            if (_environment.IsEnvironment("Development"))
            {
                builder.AddUserSecrets<Startup>();
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHeroesServices();

            if (_environment.IsEnvironment("Test"))
            {
                services.AddMemoryRepository();
            }
            else
            {
                services.AddMongoDBRepository(Configuration);                
            }

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:4200/");
            }));
            services.AddSignalR();

            services.AddMvcCore()
                .AddJsonFormatters()
                .AddApiExplorer()
                .AddAuthorization(auth =>
                {
                    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                        .RequireAuthenticatedUser().Build());
                });

            if (!_environment.EnvironmentName.StartsWith("Test"))
            {
                services.AddAuthentication("bearer")
                .AddIdentityServerAuthentication("bearer", options =>
                {
                    options.Authority = Configuration["UrlSetting:OAuthServerUrl"];
                    options.RequireHttpsMetadata = false;

                    //options.ApiName = "https://localhost:5000/resources";
                });
            }

            if (_environment.IsEnvironment("Production"))
            {
                // In production, the Angular files will be served from this directory
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });
            }

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "Beta 0.1",
                    Title = "Tour of heroes",
                    Description = "This is more for demo.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Fredrik Strandin",
                        Email = "fredrik.strandin@uniqode.se",
                        Url = "https://www.linkedin.com/in/fredrik-strandin-b8381930/"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });

                //// Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            if (_environment.IsEnvironment("Test") || _environment.IsEnvironment("Development"))
            {
                app.UseRemoveData();
                app.UseInsertData();
            }

            app.UseGlobalExceptionHandler(x =>
            {
                x.ContentType = "application/json";
                x.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    Message = "An error occurred whilst processing your request"
                }));

                x.Map<UnauthorizedAccessException>()
                    .ToStatusCode((int)HttpStatusCode.Unauthorized)
                    .WithBody((e, c) => "Unauthorized Access");

                x.Map<WebMessageException>()
                    .ToStatusCode((int)HttpStatusCode.OK)
                    .WithBody((ex, context) =>
                    {
                        context.Response.StatusCode = ((WebMessageException)ex).StatusCode;

                        return JsonConvert.SerializeObject(new
                        {
                            Message = ex.Message,
                        });
                    });

                x.OnError((exception, httpContext) =>
                {
                    _logger.LogError(exception.Message);

                    TelemetryClient telemetryClient = new TelemetryClient();

                    telemetryClient.TrackException(exception);

                    return Task.CompletedTask;
                });
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();
            if (_environment.IsEnvironment("Production"))
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notify");
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heroes API beta");
            });

            if (_environment.IsEnvironment("Production"))
            {
                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }
        }
    }
}
