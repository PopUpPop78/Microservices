using PlatformService.Models;

namespace PlatformService.Data
{
    public static class SeedDatabase
    {
        public static void PrepareDatabase(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                AddData(scope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void AddData(AppDbContext context)
        {
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