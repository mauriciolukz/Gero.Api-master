using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using IBM.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Server.IISIntegration;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Gero.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework compatibility
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // Add authentication schema
            services
                .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = IISDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer("Bearer", options =>
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.FromMinutes(5),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JWT:issuer"],
                        ValidAudience = Configuration["JWT:audience"]
                    }
                );

            // Add database context
            // Distribution staging context
            services
                .AddDbContext<DistributionContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlServer(
                            Configuration.GetConnectionString("DistributionContext"),
                            connectionOptions => connectionOptions.CommandTimeout((int) TimeSpan.FromMinutes(10).TotalSeconds)
                        );
                    }
                );

            // BPCS Context
            services
                .AddDbContext<BPCSContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BPCSContext"))
                );

            //.AddDbContext<BPCSContext>(options =>
            //    {
            //        options.UseDb2(
            //            Configuration.GetConnectionString("BPCSContext"),
            //            optionsAction =>
            //            {
            //                optionsAction.CommandTimeout(3600);
            //                optionsAction.SetServerInfo(
            //                    IBM.EntityFrameworkCore.Storage.Internal.IBMDBServerType.AS400,
            //                    IBM.EntityFrameworkCore.Storage.Internal.IBMDBServerVersion.AS400_07_02
            //                );
            //            }
            //        );
            //    }
            //);

            // Add route analyzer
            services.AddRouteAnalyzer();

            // Add memory cache
            services.AddMemoryCache();

            //Swagger
            services
                .AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new Info { Title = "Gero.API", Description = "Smart Sales API" });
                    }
                );

            services
                .AddSwaggerGen(c =>
                    {
                        c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Gero.API.xml"));
                    }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsStaging() || env.IsProduction())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //JWT
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer("/routes");
            });

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>            
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API")
            );
        }
    }
}
