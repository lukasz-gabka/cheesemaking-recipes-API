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
            services.AddScoped<DbSeeder>();
            services.AddScoped<TemplateService>();
            services.AddScoped<NoteService>();
            services.AddScoped<ErrorHandler>();
            services.AddScoped<PasswordHasher<User>>();
            services.AddScoped<UserService>();
            services.AddScoped<IValidator<RegistrationDto>, RegistrationDtoValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandler>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
