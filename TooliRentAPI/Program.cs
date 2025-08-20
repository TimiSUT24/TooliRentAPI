
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TooliRentAPI.Data;
using TooliRentAPI.Services;
using TooliRentAPI.Services.Interfaces;
using TooliRentClassLibrary.Mapper;
using TooliRentClassLibrary.Models;
using TooliRentClassLibrary.Validators;

namespace TooliRentAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IJwt, JwtService>();
            //Get ConnectionString for db
            builder.Services.AddDbContext<TooliRentDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(cfg =>
            {
                // optional: additional configuration here
            }, typeof(MappingProfile).Assembly);
          
            // Add FluentValidation 
            builder.Services.AddValidatorsFromAssemblyContaining<BookingRequestDtoValidator>();            
            builder.Services.AddFluentValidationAutoValidation();

            // Add Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TooliRentDBContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();      

            // Seed users/roles 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider; 
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var DbContext = services.GetRequiredService<TooliRentDBContext>();

                await UserRoleSeed.SeedAsync(userManager, roleManager);
                await Tool_Booking_CategorySeed.ToolAndBookingAndCategorySeed(DbContext, userManager);
            }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
