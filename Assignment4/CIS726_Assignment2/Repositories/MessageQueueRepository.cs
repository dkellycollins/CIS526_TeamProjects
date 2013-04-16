using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIS726_Assignment2.Models;
using CIS726_Assignment2.SystemBus;

namespace CIS726_Assignment2.Repositories
{
    public class MessageQueueRepository<T> : IGenericRepository<T>
        where T : IModel
    {
        private IMessageQueueProducer<T> _producer;

        public MessageQueueRepository(IMessageQueueProducer<T> producer)
        {
            _producer = producer;
        }

        public IQueryable<T> GetAll()
        {
            return _producer.GetAll().AsQueryable();
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _producer.GetAll().AsQueryable().Where(predicate);
        }

        public T Find(int id)
        {
            return null;
        }

        public T Find(T data)
        {
            return _producer.Get(data);
        }

        public void Add(T entity)
        {
            _producer.Create(entity);
        }

        public void Remove(T entity)
        {
            _producer.Remove(entity);
        }

        public void Edit(T entity)
        {
            _producer.Update(entity);
        }

        public void UpdateValues(T entity, T item)
        {
            _producer.Update(item);
        }

        public void SaveChanges()
        {
            //Automatically handled.
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}