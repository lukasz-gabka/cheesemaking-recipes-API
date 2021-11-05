using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cheesemaking_recipes_API
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
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApiConnectionString")));
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<DbMigrator>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ErrorHandler>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher <User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IValidator<RegistrationDto>, RegistrationDtoValidator>();
            services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();
            AddAuthentication(services);
            services.AddCors(options => {
                options.AddPolicy("ReactApp", builder =>
                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbMigrator migrator)
        {
            app.UseCors("ReactApp");
            migrator.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandler>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = true;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });
        }
    }
}
