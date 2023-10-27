using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : Repository<Platform>, IPlatformRepository
    {
        public PlatformRepository(AppDbContext context) :
            base(context)
        {
        }
    }
}