﻿using Microsoft.EntityFrameworkCore;
using OnlineStoreApp.Interfaces;
using OnlineStoreApp.Models;
using OnlineStoreApp.Settings;
using System.Linq.Expressions;

namespace OnlineStoreApp.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseClass
    {
        private DbSet<T> _dbSet;
        private StoreDbContext _context;

        public Repository(StoreDbContext storeDbContext)
        {
            _context = storeDbContext;
            _dbSet = _context.Set<T>();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return (await query.FirstOrDefaultAsync(expression))!;
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return (await query.AsNoTracking().ToListAsync())!;
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
