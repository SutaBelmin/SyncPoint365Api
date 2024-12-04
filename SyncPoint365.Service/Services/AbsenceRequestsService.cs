using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public class AbsenceRequestsService : BaseService<AbsenceRequest, AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>, IAbsenceRequestsService
    {
        protected readonly IAbsenceRequestsRepository _repository;
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
        public async Task<IPagedList<AbsenceRequestDTO>> GetAbsenceRequestsPagedListAsync(string? nameQuery, string? typeQuery, DateTime dateFrom, DateTime dateTo, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var paged = await _repository.GetAbsenceRequestsPagedListAsync(nameQuery, typeQuery, dateFrom, dateTo, page, pageSize, cancellationToken: cancellationToken);

            var entities = paged.ToList();
            var dtos = Mapper.Map<List<AbsenceRequestDTO>>(entities);

            return new PagedList<AbsenceRequestDTO>(paged, dtos);
        }
    }
}
