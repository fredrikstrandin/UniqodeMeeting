// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly IHostingEnvironment _environment;
        private IConfiguration _configuration { get; }

        public Startup(
            ILogger<Startup> logger,
            IHostingEnvironment environment,
            IConfiguration configuration)
        {
            _logger = logger;
            _environment = environment;
            _configuration = configuration;

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

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());

            services.AddAuthentication()
                .AddFacebook(option =>
                {
                    option.AppId = _configuration["FacebookSetting:AppId"];
                    option.AppSecret = _configuration["FacebookSetting:AppSecret"];

                    option.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    option.SaveTokens = true;
                })
                .AddTwitter(option =>
                {
                    option.ConsumerKey = _configuration["TwitterSetting:ConsumerKey"];
                    option.ConsumerSecret = _configuration["TwitterSetting:ConsumerSecret"];
                    option.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    option.SaveTokens = true;
                }); 
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}