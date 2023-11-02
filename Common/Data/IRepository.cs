using Common.Models;

namespace Common.Data
{
    public interface IRepository<T> where T : class, IModel
    {
        bool SaveChanges();
        IEnumerable<T> GetAll();
        T GetItemById(int id);
        void Create(T item);
    }
}