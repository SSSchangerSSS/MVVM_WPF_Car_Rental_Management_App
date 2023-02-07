using AutoMapper;
using Models.Exceptions;
using Models.Services.Crud;
using Models.Services.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// <see cref="GModelList{T,DTO}"/> class implements <see cref="IGModelList{T,DTO}"/> interface.
    /// </summary>
    public class GModelList<T, DTO> : IGModelList<T, DTO> where T : IIdentifier where DTO : class, IIdentifier
    {
        private readonly IGcrud<DTO> _dbset;
        private readonly IGConflictValidator<T, DTO> _gValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GModelList{T,DTO}"/> class.
        /// </summary>
        /// <param name="dbset">Implementation for crud operations.</param>
        /// <param name="gValidator">Generic validator for detecting conflicts between incoming model and existing rows.</param>
        /// <param name="mapper">AutoMapper for mapping <typeparamref name="DTO"/> to <typeparamref name="T"/>.</param>
        public GModelList(IGcrud<DTO> dbset, IGConflictValidator<T, DTO> gValidator, IMapper mapper)
        {
            _dbset = dbset;
            _gValidator = gValidator;
            _mapper = mapper;
        }


        /// <inheritdoc />
        public async Task<bool> Add(T incomingModel)
        {
            T conflictingModel = await _gValidator.GetConflictingModel(incomingModel);

            if (conflictingModel != null)
            {
                throw new GModelConflictException<T>($"Your Input:\n{incomingModel.ToString()}\nhas conflict with:\nID:{conflictingModel.Id}\n{conflictingModel.ToString()}", conflictingModel, incomingModel);
            }
            return await _dbset.Add(_mapper.Map<DTO>(incomingModel));
        }

        /// <inheritdoc />
        public async Task<bool> Delete(T incomingModel)
        {
            return await _dbset.Delete(_mapper.Map<DTO>(incomingModel));
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid incomingModelIndex)
        {
            return await _dbset.Delete(incomingModelIndex);
        }

        /// <inheritdoc />
        public async Task<bool> Update(Guid incomingModelIndex, T incomingModel)
        {
            T conflictingModel = await _gValidator.GetConflictingModel(incomingModel);

            if (conflictingModel != null)
            {
                throw new GModelConflictException<T>($"Your Input:\n{incomingModel.ToString()}\nhas conflict with:\nID:{conflictingModel.Id}\n{conflictingModel.ToString()}", conflictingModel, incomingModel);
            }
            return await _dbset.Update(incomingModelIndex, _mapper.Map<DTO>(incomingModel));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAll()
        {
            return _mapper.Map<IEnumerable<T>>(await _dbset.GetAll());
        }

        /// <inheritdoc />
        public async Task<T> GetRowByID(Guid ID)
        {
            return _mapper.Map<T>(await _dbset.GetRowByID(ID));
        }
    }

}
