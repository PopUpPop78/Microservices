using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IRepository<T> where T : class, IModel
    {
        bool SaveChanges();
        IEnumerable<T> GetAll();
        T GetPlatformById(int id);
        void Create(T item);
    }
}