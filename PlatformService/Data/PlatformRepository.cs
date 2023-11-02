using Common.Data;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : Repository<Platform, AppDbContext>, IPlatformRepository
    {
        public PlatformRepository(AppDbContext context) :
            base(context)
        {
        }
    }
}