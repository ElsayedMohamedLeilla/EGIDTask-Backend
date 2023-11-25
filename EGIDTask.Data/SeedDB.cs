using EGIDTask.Domain.Entities.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace EGIDTask.Data
{
    public class SeedDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();
            context.Database.EnsureCreated();

            var rolesToSeed = new List<Stock>
            {
                new Stock { Name = "Vianet", Price = 10.8m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Agritek", Price = 50, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Akamai", Price = 62.5m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Baidu", Price = 60, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Blinkx", Price = 44.8m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Blucora", Price = 15, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Boingo", Price = 43, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Brainybrawn", Price = 77.7m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Carbonite", Price = 34.5m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "China Finance", Price = 100, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "ChinaCache", Price = 90, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "ADR", Price = 99.5m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "ChitrChatr", Price = 10.5m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Cnova", Price = 84.6m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Cogent", Price = 50, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Crexendo", Price = 8.2m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "CrowdGather", Price = 80.6m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "EarthLink", Price = 66.8m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Eastern", Price = 50, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "ENDEXX", Price = 20m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Envestnet", Price = 20.5m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Epazz", Price = 30.3m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "FlashZero", Price = 30, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Genesis", Price = 43.6m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "InterNAP", Price = 66.3m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "MeetMe", Price = 44.3m, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Netease", Price = 97, IsActive = true,IsDeleted = false,AddedDate = DateTime.UtcNow},
                new Stock { Name = "Qihoo", Price = 57.8m}
            };
            if (!context.Stocks.Any())
            {
                context.Stocks.AddRange(rolesToSeed);
                context.SaveChanges();
            }
        }
    }
}