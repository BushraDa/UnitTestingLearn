using UnitTestingLearn.DataLayer.Models;
using UnitTestingLearn.DataLayer.Repositories.Interface;

namespace UnitTestingLearn.DataLayer.Repositories.Class
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _Context;

        public BaseRepository(ApplicationContext context)
        {
            _Context = context;
        }
        public void Create(T item)
        {
            _Context.Set<T>().Add(item);
            _Context.SaveChanges();
        }

        public void Delete(T item)
        {
            _Context.Remove(item);
            _Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _Context.Set<T>();
        }

        public T? GetById(long id)
        {
            return _Context.Set<T>().Find(id);
        }
    }
}
