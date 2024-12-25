using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public abstract class BaseService<TEntity, TDTO, TAddDTO, TUpdateDTO> : IBaseService<TDTO, TAddDTO, TUpdateDTO>
       where TEntity : BaseEntity
       where TDTO : BaseDTO
       where TAddDTO : BaseAddDTO
       where TUpdateDTO : BaseUpdateDTO
    {
        protected readonly IMapper Mapper;
        protected readonly IValidator<TAddDTO> AddValidator;
        protected readonly IBaseRepository<TEntity> Repository;
        protected readonly IValidator<TUpdateDTO> UpdateValidator;

        protected BaseService(IBaseRepository<TEntity> repository, IMapper mapper, IValidator<TAddDTO> addValidator, IValidator<TUpdateDTO> updateValidator)
        {
            Mapper = mapper;
            Repository = repository;
            AddValidator = addValidator;
            UpdateValidator = updateValidator;
        }

        public virtual async Task<IPagedList<TDTO>> GetAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var pagedList = await Repository.GetAsync(query, page, pageSize, cancellationToken);
            var dtos = Mapper.Map<List<TDTO>>(pagedList);

            return new PagedList<TDTO>(pagedList, dtos);
        }

        public virtual async Task<TDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            return Mapper.Map<TDTO?>(entity);
        }

        public virtual async Task AddAsync(TAddDTO dto, CancellationToken cancellationToken = default)
        {
            await AddValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = Mapper.Map<TEntity>(dto);
            await Repository.AddAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(TUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            await UpdateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = Mapper.Map<TEntity>(dto);
            Repository.Update(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteAsync(id, cancellationToken);
        }
    }
}
