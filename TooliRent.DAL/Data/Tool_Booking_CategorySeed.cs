using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Data
{
    public static class Tool_Booking_CategorySeed
    {
        public static async Task ToolAndBookingAndCategorySeed(TooliRentDBContext context, UserManager<ApplicationUser> userManager)
        {
            var users = userManager.Users.ToListAsync().Result;
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
                    context.Categories.AddRange(categories);
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
                    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Power Tools")?.Id ?? 0
                },
                new Tool
                {
                    Name = "Hammer",
                    Description = "Heavy-duty hammer for construction work.",
                    Quantity = 5,
                    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Hand Tools")?.Id ?? 0
                },
                new Tool
                {
                    Name = "Lawn Mower",
                    Description = "Gas-powered lawn mower for maintaining your lawn.",
                    Quantity = 2,
                    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Gardening Tools")?.Id ?? 0
                },
                new Tool
                {
                    Name = "Concrete Mixer",
                    Description = "Electric concrete mixer for construction projects.",
                    Quantity = 1,
                    CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Construction Tools")?.Id ?? 0
                }
            };
            try
            {
                if (!context.Tools.Any())
                {
                    context.Tools.AddRange(tools);
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
                    ToolId = context.Tools.FirstOrDefault(t => t.Name == "Drill")?.Id ?? 0,                   
                    UserId = users[0].Id, // Assuming the first user exists
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3)
                },
                new Booking
                {
                    ToolId = context.Tools.FirstOrDefault(t => t.Name == "Hammer")?.Id ?? 0,
                    UserId =  users[0].Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2)
                }
            };
            try
            {
                if (!context.Bookings.Any())
                {
                    context.Bookings.AddRange(bookings);
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
