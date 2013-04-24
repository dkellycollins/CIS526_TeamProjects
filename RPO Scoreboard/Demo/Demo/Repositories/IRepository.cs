using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Repositories
{
    /// <summary>
    /// Main repository interface.
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface IRepository<T> : IDisposable
    {
        /// <summary>
        /// Retrives a single entity from the database.
        /// </summary>
        /// <param name="entityID">ID of the entity.</param>
        /// <returns>Entity</returns>
        T Get(int entityID);

        /// <summary>
        /// Retrives all entities from the database.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        
        /// <summary>
        /// Creates a new entity in database.
        /// </summary>
        /// <param name="entityToCreate">Entity with the info to create.</param>
        void Create(T entityToCreate);

        /// <summary>
        /// Updates the entity in the database.
        /// </summary>
        /// <param name="entityToUpdate">Entity with the info the update.</param>
        void Update(T entityToUpdate);

        /// <summary>
        /// Deletes the entity in the database with the same id.
        /// </summary>
        /// <param name="entityID">ID of the entity to delete.</param>
        void Delete(int entityID);
    }
}