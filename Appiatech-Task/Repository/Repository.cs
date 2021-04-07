using Appiatech_Task.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appiatech_Task.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> predicate = null);
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        int Count(Func<T, bool> predicate = null);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppiatechDBContext db;

        public Repository(AppiatechDBContext dbContext)
        {
            db = dbContext;
        }
        public IEnumerable<T> GetAll(Func<T,bool> predicate = null)
        {
            if(predicate != null)
            {
                return db.Set<T>().Where(predicate);
            }
            return db.Set<T>();
        }
        public async Task<T> GetById(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }
        public async Task Create(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task Remove(T entity)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }
        public int Count(Func<T, bool> predicate = null)
        {
            if (predicate != null)
            {
                return db.Set<T>().Count(predicate);
            }
            return db.Set<T>().Count();
        }
    }
}
