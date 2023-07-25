using UnitTestingLearn.DataLayer.Models;

namespace UnitTestingLearn.DataLayer.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T? GetById(long id);
        public void Create(T item);
        public void Delete(T item);
    }
}
