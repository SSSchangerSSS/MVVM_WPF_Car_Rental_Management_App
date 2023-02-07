using System.Threading.Tasks;

namespace Models.Services.Validators
{
    /// <summary>
    /// This generic interface is a validator for detecting whether an incoming <typeparamref name="T"/> model has conflict with an existing <typeparamref name="DTO"/> in dbset or doesn't 
    /// </summary>
    public interface IGConflictValidator<T,DTO>
    {
        /// <summary>
        /// returns a row mapped to <typeparamref name="T"/> if incoming <typeparamref name="T"/> model has conflict with a row in <typeparamref name="DTO"/> dbset
        /// </summary>
        /// <returns>The conflicting row mapped to <typeparamref name="T"/>.</returns>
        Task<T> GetConflictingModel(T model);
    }
}
