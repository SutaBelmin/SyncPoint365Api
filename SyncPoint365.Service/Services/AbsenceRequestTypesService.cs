using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

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

        public override async Task AddAsync(AbsenceRequestTypeAddDTO dto, CancellationToken cancellationToken = default)
        {
            await AddValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = Mapper.Map<AbsenceRequestType>(dto);

            await Repository.AddAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesAsync(CancellationToken cancellationToken = default)
        {
            var absenceRequestTypes = await _repository.GetAbsenceRequestTypesListAsync();

            return _mapper.Map<IEnumerable<AbsenceRequestTypeDTO>>(absenceRequestTypes);
        }
    }
}

