using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.DAL.Models;

namespace Airport.DAL.Abstractions
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T createEntity);
        Task<bool> Update(T updateEntity);
        Task<bool> Delete(int id);
    }
}