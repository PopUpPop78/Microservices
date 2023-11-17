using Common.Data;
using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepository : IRepository<Command>
    {
        // Platforms
        IEnumerable<Platform> GetPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        // Commands
        IEnumerable<Command> GetCommandsByPlatformId(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    } 
}