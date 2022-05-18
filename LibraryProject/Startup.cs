<<<<<<< HEAD
using LibraryProject.API.Authorization;
using LibraryProject.API.Helpers;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
=======
using LibraryProject.Database;
using LibraryProject.Repositories;
using LibraryProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
>>>>>>> Bilal_Branch
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace LibraryProject.API
{
    public class Startup
    {
        private readonly string CORSRules = "_CORSRules";
<<<<<<< HEAD
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
=======
        public Startup(IConfiguration configuration)
>>>>>>> Bilal_Branch
        {
            _env = env;
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORSRules,
                    builder =>
                    {
                        //builder.WithOrigins("http://localhost:4200")
                        builder.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                    });

            });

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));//den henter appsettings fra json 

            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            /*
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanService, LoanService>();
            */
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();


            services.AddDbContext<LibraryProjectContext>(
              o => o.UseSqlServer(_configuration.GetConnectionString("Default")));

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (se.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            });


            services.AddDbContext<LibraryProjectContext>(
                x => x.UseSqlServer(Configuration.GetConnectionString("Default")));
            
            
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoanRepository, LoanRepository>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryProject.API", Version = "v1" });
                // To Enable authorization using Swagger (JWT)  
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }

                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryProject v1"));

            }


            app.UseHttpsRedirection();
            app.UseCors(CORSRules);

            app.UseCors(CORSRules);
            app.UseRouting();
       
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
