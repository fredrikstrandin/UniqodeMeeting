using HeroesWeb.Repositorys;
using HeroesWeb.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace HeroesWeb
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<IHeroRepository, HeroRepository>();

            services.AddMvcCore() //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                .AddJsonFormatters()
                .AddAuthorization(auth =>
                {
                    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                        .RequireAuthenticatedUser().Build());
                });

            services.AddAuthentication("bearer")
                    .AddIdentityServerAuthentication("bearer", options =>
                    {
                        options.Authority = "https://localhost:5000";
                        options.RequireHttpsMetadata = false;

                        options.ApiName = "https://localhost:5000/resources";
                        //options.ApiSecret = "secret";
                        
                        options.JwtBearerEvents = new JwtBearerEvents
                        {
                            OnMessageReceived = e =>
                            {
                                _logger.LogTrace("JWT: message received");
                                return Task.CompletedTask;
                            },

                            OnTokenValidated = e =>
                            {
                                _logger.LogTrace("JWT: token validated");
                                return Task.CompletedTask;
                            },

                            OnAuthenticationFailed = e =>
                            {
                                _logger.LogTrace("JWT: authentication failed");
                                return Task.CompletedTask;
                            },

                            OnChallenge = e =>
                            {
                                _logger.LogTrace("JWT: challenge");
                                return Task.CompletedTask;
                            }
                        };
                    });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
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

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

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
