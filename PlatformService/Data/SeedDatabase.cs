using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class SeedDatabase
    {
        public static void PrepareDatabase(IApplicationBuilder app, bool isProd)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                AddData(scope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void AddData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                try 
                {
                    Console.WriteLine("Applying migrations");
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error occured during migrations: {ex.Message}");
                }
            }

            if(!context.Platforms.Any())
            {
                Console.WriteLine("Adding data");

                context.Platforms.AddRange(
                    new Platform
                    {
                        Name = "Microsoft .Net",
                        Publisher = "Microsoft",
                        Cost = "Pricey"
                    },
                    new Platform
                    {
                        Name = "Linux",
                        Publisher = "Linux",
                        Cost = "Free"
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Data already exists");
            }
        }
    }    
}