using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    /// <summary>
    /// An interface for the Producer to push messages onto a queue.
    /// </summary>
    /// <typeparam name="T">Model type.</typeparam>
    public interface IMessageQueueProducer<T>
        : IDisposable
    {
        /// <summary>
        /// Retrives a single model from the data base.
        /// </summary>
        /// <param name="data">A model that contains some or all of the information to get.</param>
        /// <returns></returns>
        T Get(T data);

        /// <summary>
        /// Retives all models from the data base.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Updates all models contained in the list.
        /// </summary>
        /// <param name="data"></param>
        void Update(T data);

        /// <summary>
        /// Creates all models in the database.
        /// </summary>
        /// <param name="data"></param>
        List<T> Create(T data);

        /// <summary>
        /// Removes all models contains in the list from the database.
        /// </summary>
        /// <param name="data"></param>
        void Remove(T data);
    }
}