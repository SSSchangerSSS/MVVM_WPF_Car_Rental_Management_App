using Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Services.Crud
{
    /// <summary>
    /// This is a genric interface for crud operations on database
    /// </summary>
    public interface IGcrud<T> where T : class, IIdentifier
    {
        /// <summary>
        /// Add a row to dbset <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity">The incoming entity to be added to the dbset.</param>
        /// <returns>Adding was successfull or not as a boolean.</returns>
        Task<bool> Add(T entity);
        /// <summary>
        /// Fetch all rows from dbset <typeparamref name="T"/> and return them all as an IEnumerable
        /// </summary>
        /// <returns>All rows in dbset as an IEnumerable</returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Fetch a row from dbset <typeparamref name="T"/>  by its id and return it
        /// </summary>
        /// <param name="ID">The ID of row to get</param>
        /// <returns>A row according to the given id from its dbset</returns>
        Task<T> GetRowByID(Guid ID);
        /// <summary>
        /// Add a row to dbset <typeparamref name="T"/>
        /// </summary>
        /// <param name="id">The old row ID to be updated in the dbset.</param>
        /// <param name="entity">The incoming entity to be replaced with old row in the dbset.</param>
        /// <returns>updating was successfull or not as a boolean.</returns>
        Task<bool> Update(Guid id, T entity);
        /// <summary>
        /// Delete a row dbset dbset <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity">The incoming entity to be deleted from the dbset.</param>
        /// <returns>deleting was successfull or not as a boolean.</returns>
        Task<bool> Delete(T entity);
        /// <summary>
        /// Delete a row dbset dbset <typeparamref name="T"/>
        /// </summary>
        /// <param name="id">The id of entity to be deleted from the dbset.</param>
        /// <returns>deleting was successfull or not as a boolean.</returns>
        Task<bool> Delete(Guid id);
    }
}
