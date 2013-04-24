using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.Models;

namespace Demo.Repositories
{
    public class BasicRepo<T> : IRepository<T> where T : class
    {
        private MasterContext _context;

        public BasicRepo()
        {
            _context = new MasterContext();
        }

        public T Get(int entityID)
        {
            return _context.Set<T>().Find(entityID);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Create(T entityToCreate)
        {
            _context.Set<T>().Add(entityToCreate);
            _context.SaveChanges();
        }

        public void Update(T entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = System.Data.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int entityID)
        {
            T entity = Get(entityID);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}