using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SampleApi.Data;
using Microsoft.EntityFrameworkCore;
using SampleApi.Data.Repository;
using SampleApi.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SampleApi.RestAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SampleApi.RestAPI.Helpers;
using Microsoft.OpenApi.Models;
using System.IO;
using Swashbuckle.AspNetCore.Filters;

namespace SampleApi.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:8080")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            var domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = Configuration["Auth0:Audience"];
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("add:apartments", policy => policy.Requirements.Add(new HasScopeRequirement("add:apartments", domain)));
                options.AddPolicy("add:locations", policy => policy.Requirements.Add(new HasScopeRequirement("add:locations", domain)));
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => {
                    //Note: disable default ProblemDetails response
                    options.SuppressMapClientErrors = true;

                    // set ApiErrorResponse to be returned for Model Validation errors
                    options.InvalidModelStateResponseFactory = context => {
                        var actionExecutingContext = context as ActionExecutingContext;
                        if (context.ModelState.ErrorCount > 0 && actionExecutingContext?.ActionArguments.Count ==
                        context.ActionDescriptor.Parameters.Count)
                        {
                            return new UnprocessableEntityObjectResult(context.ModelState.ToApiErrorResponse(DefinedApiErrorCodes.InputValidation));
                        }
                        return new BadRequestObjectResult(context.ModelState.ToApiErrorResponse(DefinedApiErrorCodes.ProblemParsing));
                    };
                });


            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var connectionString = Configuration.GetValue<string>("SampleApiConnectionString");
            services.AddDbContext<SampleApiContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IApartmentsRepository, ApartmentsRepository>()
                .AddTransient<ILocationsRepository, LocationsRepository>();

            services.AddSwaggerGen(setupAction => {
                setupAction.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = "Smart Apartment Data API",
                        Version = "1",
                        Description = "API for managing apartments",
                        Contact = new OpenApiContact
                        {
                            Email = "support@website.com",
                            Name = "Support team"
                        }
                    });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "SampleApiAPI.xml");
                setupAction.IncludeXmlComments(filePath); 
                setupAction.OperationFilter<AddResponseHeadersFilter>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(
                    setupAction =>
                    {
                        setupAction.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "Smart Apartment Data API");
                        setupAction.RoutePrefix = "";
                    });


            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
