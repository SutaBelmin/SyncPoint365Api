using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class AbsenceRequestsService : BaseService<AbsenceRequest, AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>, IAbsenceRequestsService
    {
        private readonly IAbsenceRequestsRepository _repository;
        protected readonly IMapper _mapper;

        public AbsenceRequestsService(IAbsenceRequestsRepository repository,
            IMapper mapper,
            IValidator<AbsenceRequestAddDTO> addValidator,
            IValidator<AbsenceRequestUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AbsenceRequestDTO>> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default)
        {
            var absenceRequests = await _repository.GetAbsenceRequestsListAsync();
            return _mapper.Map<IEnumerable<AbsenceRequestDTO>>(absenceRequests);
        }

    }
}
