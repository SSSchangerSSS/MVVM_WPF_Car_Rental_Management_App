using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Exceptions;

namespace Models.Models
{
    /// <summary>
    /// This interface is a genric list for crud operations on database according to Models
    /// </summary>
    public interface IGModelList<T,DTO> where T : IIdentifier where DTO : class, IIdentifier
    {
        /// <summary>
        /// Add a row to dbset <typeparamref name="DTO"/> according to <typeparamref name="T"/> incomingModel (<typeparamref name="DTO"/> is data transfer object for <typeparamref name="T"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <typeparamref name="T"/> model has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="incomingModel">The incoming model to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{T}"></exception>
        Task<bool> Add(T incomingModel);
        /// <summary>
        /// Delete a row from dbset <typeparamref name="DTO"/> according to <typeparamref name="T"/> incomingModel (<typeparamref name="DTO"/> is data transfer object for <typeparamref name="T"/> model)
        /// </summary>
        /// <param name="incomingModel">The incoming model to be deleted from the dbset.</param>
        /// <returns>Deleting was successfull or not as a boolean.</returns>
        Task<bool> Delete(T incomingModel);
        /// <summary>
        /// Delete a row from dbset <typeparamref name="DTO"/> according to <typeparamref name="T"/> incomingModelIndex (<typeparamref name="DTO"/> is data transfer object for <typeparamref name="T"/> model)
        /// </summary>
        /// <param name="incomingModelIndex">The ID of model to be deleted from the dbset.</param>
        /// <returns>Deleting was successfull or not as a boolean.</returns>
        Task<bool> Delete(Guid incomingModelIndex);
        /// <summary>
        /// Update an existing row from dbset <typeparamref name="DTO"/> according to <typeparamref name="T"/> incomingModel (<typeparamref name="DTO"/> is data transfer object for <typeparamref name="T"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <typeparamref name="T"/> model has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="incomingModelIndex">The old row ID to be updated in the dbset.</param>
        /// <param name="incomingModel">The incoming model to be replaced with old row in the dbset.</param>
        /// <returns>Adding was successfull or not as a boolean.</returns>
        /// <exception cref="GModelConflictException{T}"></exception>
        Task<bool> Update(Guid incomingModelIndex,T incomingModel);
        /// <summary>
        /// Fetch all rows from dbset <typeparamref name="DTO"/> and return them all after mapping them to a list of <typeparamref name="T"/> (<typeparamref name="DTO"/> is data transfer object for <typeparamref name="T"/> model)
        /// </summary>
        /// <returns>All rows in dbset mapped to modelLiset</returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Fetch a row from dbset <typeparamref name="DTO"/>  by its id and return it after mapping it to a <typeparamref name="T"/> (<typeparamref name="DTO"/> is data transfer object for <typeparamref name="T"/> model)
        /// </summary>
        /// <param name="ID">The ID of row to get</param>
        /// <returns>A row mapped to a model according to the given id</returns>
        Task<T> GetRowByID(Guid ID);
    }
}
