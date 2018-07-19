using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(T createEntity)
        {
            await _dbSet.AddAsync(createEntity);
        }

        public async Task<bool> Update(T updateEntity)
        {
            var existingEntity = await _dbSet.FindAsync(updateEntity.Id);
            return await Task.Run(() =>
            {
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                    _context.Entry(updateEntity).State = EntityState.Modified;
                    return true;
                }

                return false;
            });
        }

        public async Task<bool> Delete(int id)
        {
            T getObjById = await _dbSet.FindAsync(id);
            if (getObjById != null)
            {
                _dbSet.Remove(getObjById);
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
            _dbSet = null;
        }
    }
}
