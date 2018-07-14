using System.Collections.Generic;
using System.Linq;
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

        public T Get(int id)
        {
            return SeedData.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return SeedData;
        }

        public void Insert(T createEntity)
        {
            createEntity.Id = SeedData.Max(x => x.Id);
            SeedData.Add(createEntity);
        }

        public abstract bool Update(T updateEntity);

        public bool Delete(int id)
        {
            var entityToDelete = SeedData.FirstOrDefault(x => x.Id == id);
            if (entityToDelete == null)
                return false;

            SeedData.Remove(entityToDelete);
            return true;
        }

        public void Save()
        {

        }
    }
}