using System;
using System.Collections.Generic;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport.DAL.Repositories.EntityFramework
{
    public class Repository<T> : IDisposable, IRepository<T> where T : Entity
    {
        private DbContext _context;
        private DbSet<T> _dbSet;


        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T createEntity)
        {
            _dbSet.Add(createEntity);
        }

        public bool Update(T updateEntity)
        {
            _context.Entry(updateEntity).State = EntityState.Modified;
            return true;
        }

        public bool Delete(int Id)
        {
            T getObjById = _dbSet.Find(Id);
            _dbSet.Remove(getObjById);
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
            _dbSet = null;
        }
    }
}
