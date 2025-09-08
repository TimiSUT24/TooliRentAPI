using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Data
{
    public static class Tool_Booking_CategorySeed
    {
        public static async Task ToolAndBookingAndCategorySeed(TooliRentDBContext context, UserManager<ApplicationUser> userManager)
        {
            var users = await userManager.Users.ToListAsync();
            try
            {
                // Seed Categories 
                var categories = new List<Category>
                {
                new Category { Name = "Power Tools" },
                new Category { Name = "Hand Tools" },
                new Category { Name = "Gardening Tools" },
                new Category { Name = "Construction Tools" }
                };

                if (!context.Categories.Any())
                {
                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seeding failed" + ex.Message);   
            }
            
            // Seed Tools 
            var tools = new List<Tool>
            {
                new Tool
                {
                    Name = "Drill",
                    Description = "Cordless drill for various drilling tasks.",
                    Quantity = 3,
                    CategoryId = context.Categories.First(c => c.Name == "Power Tools").Id
                },
                new Tool
                {
                    Name = "Hammer",
                    Description = "Heavy-duty hammer for construction work.",
                    Quantity = 5,
                    CategoryId = context.Categories.First(c => c.Name == "Hand Tools").Id
                },
                new Tool
                {
                    Name = "Lawn Mower",
                    Description = "Gas-powered lawn mower for maintaining your lawn.",
                    Quantity = 2,
                    CategoryId = context.Categories.First(c => c.Name == "Gardening Tools").Id
                },
                new Tool
                {
                    Name = "Concrete Mixer",
                    Description = "Electric concrete mixer for construction projects.",
                    Quantity = 1,
                    CategoryId = context.Categories.First(c => c.Name == "Construction Tools").Id
                }
            };
            try
            {
                if (!context.Tools.Any())
                {
                    await context.Tools.AddRangeAsync(tools);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seeding failed" + ex.Message);
            }           
             
             
            // Seed Bookings 
            var bookings = new List<Booking>
            {
                new Booking
                {
                    ToolId = context.Tools.First(t => t.Name == "Drill").Id,                   
                    UserId = users[1].Id, // Assuming the first user exists
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3)
                },
                new Booking
                {
                    ToolId = context.Tools.First(t => t.Name == "Hammer").Id,
                    UserId =  users[1].Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2)
                }
            };
            try
            {
                if (!context.Bookings.Any())
                {
                    await context.Bookings.AddRangeAsync(bookings);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seeding failed" + ex.Message);
            }
            
            
        }
    }
}
