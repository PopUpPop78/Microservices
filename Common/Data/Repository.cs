using Common.Data;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data
{
    public abstract class Repository<T, TContext> : IRepository<T>
        where T : class, IModel
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public Repository(TContext context)
        {
            Context = context;
        }

        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Context.Set<T>().Add(item);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().AsEnumerable();
        }

        public virtual T GetItemById(int id)
        {
            return (from x in Context.Set<T>() where x.Id == id select x).FirstOrDefault();
        }

        public virtual bool SaveChanges()
        {
            return Context.SaveChanges() >= 0;
        }
    }
}