using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;

namespace Airport.DAL.Repositories.Seeds
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly List<T> SeedData;

        protected BaseRepository()
        {
            SeedData = new List<T>();
        }

        public async Task<T> Get(int id)
        {
            var result = new Task<T>(() => SeedData.FirstOrDefault(x => x.Id == id));
            return await result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => SeedData);
        }

        public async Task Insert(T createEntity)
        {
            await Task.Run(() =>
            {
                createEntity.Id = SeedData.Max(x => x.Id);
                SeedData.Add(createEntity);
            });
        }

        public abstract Task<bool> Update(T updateEntity);

        public async Task<bool> Delete(int id)
        {
            return await Task.Run(() => 
            {
                var entityToDelete = SeedData.FirstOrDefault(x => x.Id == id);
                if (entityToDelete == null)
                    return false;

                SeedData.Remove(entityToDelete);
                return true;
            });
        }
    }
}