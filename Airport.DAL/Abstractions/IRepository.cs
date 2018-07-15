using System.Collections.Generic;
using Airport.DAL.Models;

namespace Airport.DAL.Abstractions
{
    public interface IRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Insert(T createEntity);
        bool Update(T updateEntity);
        bool Delete(int id);
        void Save();
    }
}