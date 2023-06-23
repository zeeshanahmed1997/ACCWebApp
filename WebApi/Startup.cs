using System;
using Application.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Infrastructure;
using Domain.Models;
using Domain.Repositories;
namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Load connection string from the local project's appsettings.json file
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Register DbContext
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(connectionString));

            // Register repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClothingRepository, ClothingRepository>();
            services.AddScoped<IClothingTypeRepository, ClothingTypeRepository>();
            services.AddScoped<IFabricRepository, FabricRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            // Register services
            services.AddScoped<ProductService>();
            services.AddScoped<ClothingService>();
            services.AddScoped<FabricService>();
            services.AddScoped<GenderService>();
            services.AddScoped<ClothingTypeService>();
            services.AddScoped<UserService>();
            // ... other service and repository registrations

            services.AddControllers();

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            // Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
