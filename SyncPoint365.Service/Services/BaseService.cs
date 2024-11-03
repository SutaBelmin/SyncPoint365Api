using AutoMapper;
using SyncPoint365.Core.DTOs;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public abstract class BaseService<TEntity, TDTO, TAddDTO> : IBaseService<TDTO>
        where TEntity : BaseEntity
        where TDTO : BaseDTO
    {
        protected readonly IBaseRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        protected BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public virtual async Task<TDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            return Mapper.Map<TDTO?>(entity);
        }

        public virtual async Task AddAsync(TAddDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = Mapper.Map<TEntity>(dto);
            await Repository.AddAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }
    }
}
