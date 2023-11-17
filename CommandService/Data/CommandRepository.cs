using CommandService.Models;
using PlatformService.Data;

namespace CommandService.Data
{
    public class CommandRepository : Repository<Command, AppDbContext>, ICommandRepository
    {
        public CommandRepository(AppDbContext context) :
            base(context)
        {
            
        }

        public void CreateCommand(int platformId, Command command)
        {
            if(command == null)
                throw new ArgumentException(null, nameof(command));

            command.PlatformId = platformId;
            Context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
                throw new ArgumentNullException(nameof(platform));

            Context.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return (from x in Context.Platforms where x.ExternalId == externalPlatformId select x).Any();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return (from x in Context.Commands where x.Id == commandId && x.PlatformId == platformId select x).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsByPlatformId(int platformId)
        {
            return from x in Context.Commands where x.PlatformId == platformId select x;
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return Context.Platforms.AsEnumerable();
        }

        public bool PlatformExists(int platformId)
        {
            return (from x in Context.Platforms where x.Id == platformId select x).Any();
        }
    }
}