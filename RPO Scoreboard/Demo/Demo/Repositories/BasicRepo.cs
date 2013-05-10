using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Repositories
{
    public class BasicRepo<T> : IRepository<T> where T : class
    {
        private MasterContext _context;

        public BasicRepo()
        {
            _context = MasterContext.Instance;
        }

        public T Get(int entityID)
        {
            return _context.Set<T>().Find(entityID);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> selector)
        {
            return _context.Set<T>().Where(selector);
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