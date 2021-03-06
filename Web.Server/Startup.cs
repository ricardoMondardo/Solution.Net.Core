﻿using AspNetCore.IServiceCollection.AddIUrlHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.EmailSender.Interfaces;
using Web.EmailSender;
using Web.Repository.Context;
using Web.Core.Interfaces;
using Web.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Web.Server.Services.Interface;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.IdentityModel.Logging;
using System;
using Web.Repository;

namespace Web.Server
{
    public class Startup
    {

        private readonly IHostingEnvironment Env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            #region Add Database Context
            if (Env.IsDevelopment())
            {
                services.AddDbContext<DataBaseContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DevConnection")));
            }
            else
            {
                services.AddDbContext<DataBaseContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("ProdConnection")));
            }
            #endregion

            #region Authentication
            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //The issuer is the actual server that created the token
                    ValidateAudience = true,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "rmondardo",
                    ValidAudience = "rmondardo",

                    ClockSkew = TimeSpan.Zero, //Expires right after expiration date

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JWTSecretKey"))
                    )
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];                            
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<IAuthService>(
                new AuthService(
                    Configuration.GetValue<string>("JWTSecretKey"),
                    Configuration.GetValue<int>("JWTLifespan")
                )
            );
            #endregion

            services.AddHttpClient();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITodoService, TodoService>();

            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();

            services.AddUrlHelper();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IUnitOfWork unitOfWork)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
             );

            app.UseAuthentication();
            app.UseStaticFiles();

            //app.UseGraphQL();

            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=PageHome}/{action=Index}/{id?}"
                    );
            });            

        }
    }
}
