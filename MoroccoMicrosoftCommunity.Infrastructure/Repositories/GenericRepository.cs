using Microsoft.EntityFrameworkCore;
using MoroccoMicrosoftCommunity.Application.Interface;
using MoroccoMicrosoftCommunity.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDBContext _appdbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDBContext appdbContext)
        {
            _appdbContext = appdbContext;
            _dbSet = appdbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            var addentity = _dbSet.Add(entity);
            await _appdbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                throw new InvalidOperationException($"Entity with id {id} not found for deletion.");

            }
            _dbSet.Remove(entity);
            await _appdbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            if (entities == null || !entities.Any())
            {
                throw new InvalidOperationException("No entities found.");
            }

            return entities;
        }
        public async Task<T> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException("No entities found.");
            }
            return entity;
        }

        public async Task<bool> IsExists(string? key, int? value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }
            if (value == null)
            {
                throw new ArgumentException("Value cannot be null.", nameof(value));
            }


            return await _dbSet.AnyAsync(e => EF.Property<int>(e, key) == value);

        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _appdbContext.SaveChangesAsync();

        }
    }
}


