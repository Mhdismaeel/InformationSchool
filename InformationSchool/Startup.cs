using InformationSchool.Context;
using InformationSchool.Helper;
using InformationSchool.Model.Auth;
using InformationSchool.Repository.AuthService;
using InformationSchool.Repository.Interfaces.Admin;
using InformationSchool.Repository.Services.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;



namespace InformationSchool
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

           

           services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
              {
                  builder.AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(origin => true); // allow any origin
                         
              }));


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("LocalConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InformationSchool", Version = "v1" });
            });
            services.AddControllers();

            //----------------loop disabled
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //-----------------------------return error type message


            services.AddMvc()
        .ConfigureApiBehaviorOptions(options => {
            //options.SuppressModelStateInvalidFilter = true;
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errorMes = "";
                var modelState = actionContext.ModelState.Values;

                foreach (var s in modelState)
                {
                    foreach (var m in s.Errors)
                    {
                        errorMes = m.ErrorMessage;
                    }

                }
                return new BadRequestObjectResult(new { ErrorMessage = errorMes });

            };
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //-------------------------------------Add JWT Service

            services.Configure<JWT>(Configuration.GetSection("JWT"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });

            //----------------------------------------------------
            
            

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InformationSchool v1"));
            }



            app.UseHttpsRedirection();

          
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
