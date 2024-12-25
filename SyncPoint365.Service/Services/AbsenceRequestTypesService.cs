using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public class AbsenceRequestTypesService : BaseService<AbsenceRequestType, AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>, IAbsenceRequestTypesService
    {
        private readonly IAbsenceRequestTypesRepository _repository;
        protected readonly IMapper _mapper;
        public AbsenceRequestTypesService(IAbsenceRequestTypesRepository repository,
            IMapper mapper,
            IValidator<AbsenceRequestTypeAddDTO> addValidator,
            IValidator<AbsenceRequestTypeUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesListAsync(bool? isActive, CancellationToken cancellationToken = default)
        {
            var absenceRequestTypes = await _repository.GetAbsenceRequestTypesListAsync(isActive);

            return _mapper.Map<IEnumerable<AbsenceRequestTypeDTO>>(absenceRequestTypes);
        }

        public async Task<IPagedList<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesPagedListAsync(bool? isActive, string? query, string? orderBy,
            int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var paged = await _repository.GetAbsenceRequestTypesPagedListAsync(isActive, query, orderBy, page, pageSize, cancellationToken: cancellationToken);

            var dtos = Mapper.Map<List<AbsenceRequestTypeDTO>>(paged);

            return new PagedList<AbsenceRequestTypeDTO>(paged, dtos);
        }
    }
}

