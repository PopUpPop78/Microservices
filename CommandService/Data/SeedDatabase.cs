using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data
{
    public static class SeedDatabase
    {
        public static void PrepareDatabase(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = scope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient.ReturnAllPlatforms();

                SeedData(scope.ServiceProvider.GetService<ICommandRepository>(), platforms);
            }
        }

        public static void SeedData(ICommandRepository repo, IEnumerable<Platform> platforms)
        {
            Console.Write($"Seeding database with {platforms.Count()} platforms");
            foreach(var platform in platforms)
            {
                Console.WriteLine($"Creating platform {platform.Name}");
                if(!repo.PlatformExists(platform.ExternalId))
                    repo.CreatePlatform(platform);

                repo.SaveChanges();
            }
        }
    }
}