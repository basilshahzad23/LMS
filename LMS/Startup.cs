using LMS.AccountRepo;
using LMS.Data;
using LMS.Model;
using LMS.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Moq;
//using screenPrint.API.data;
//using screenPrint.API.Models;
//using screenPrint.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
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
            services.AddDbContext<LmsDBContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("LMSDB")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LmsDBContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {

                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))

                    };

                });


            services.AddControllers();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookTypeRepository, BookTypeRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IStudentFacultyRepository, Student_FacultyRepository>();
            services.AddTransient<IBookLedgerRepository, BookLedgerRepository>();
            //services.AddTransient<IUserTypeRepository, UserTypeRepository>();
            //services.AddTransient<IBookRequestLedgerRepository, BookRequestLedgerRepository>();

            //services.AddTransient<IshippingAddressRepository, shippingAddressRepository>();
            //services.AddTransient<IbillingAddressRepository, billingAddressRepository>();



            services.AddCors(options =>
            {

                options.AddDefaultPolicy(builder =>
                {

                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

                });

            });




            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}