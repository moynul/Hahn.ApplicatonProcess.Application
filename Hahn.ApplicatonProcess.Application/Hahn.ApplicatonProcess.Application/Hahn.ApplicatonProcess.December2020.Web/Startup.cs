using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.Interface;
using Hahn.ApplicatonProcess.December2020.Data.Repository;
using FluentValidation.AspNetCore;
using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Validation;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace Hahn.ApplicatonProcess.December2020.Web
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
            services.AddControllersWithViews().AddFluentValidation(); 
            services.AddDbContext<ApplicantDBContext>(options => options.UseInMemoryDatabase(databaseName: "Applicant"));            
            services.AddScoped<IApplicant, ApplicantRepository>();
            services.AddTransient<IValidator<Applicant>, ApplicantValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hahn ApplicatonProcess API",
                    Description = "Hahn ApplicatonProcess API",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn Application Api ");
            });
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            #region "Example Data"          
            var options = new DbContextOptionsBuilder<ApplicantDBContext>()
                        .UseInMemoryDatabase(databaseName: "Applicant")
                        .Options;

            using (var context = new ApplicantDBContext(options))
            {
                var applicant = new Domain.Entities.Applicant
                {
                    Id = 1,
                    Name = "Md Moynul",
                    FamilyName = "Biswas",
                    CountryOfOrigin = "Bangladesh",
                    Address = "Banasree, Dhaka, Bangladesh",
                    EmailAdress = "bappyist@gmail.com",
                    Age = 41,
                    Hired = true
                };

                context.Applicants.Add(applicant);
                context.SaveChanges();
            }
            #endregion
        }
    }
}
