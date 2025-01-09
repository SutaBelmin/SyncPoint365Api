using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;
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
        public async Task<IPagedList<AbsenceRequestDTO>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, int? absenceRequestStatusId,
            DateTime? dateFrom, DateTime? dateTo, int? year, string? orderBy, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var paged = await _repository.GetAbsenceRequestsPagedListAsync(absenceRequestTypeId, userId, absenceRequestStatusId, dateFrom, dateTo, year, orderBy, page, pageSize, cancellationToken: cancellationToken);

            var dtos = Mapper.Map<List<AbsenceRequestDTO>>(paged);

            return new PagedList<AbsenceRequestDTO>(paged, dtos);
        }

        public async Task<AbsenceRequestStatus> ChangeAbsenceRequestStatusAsync(int id, AbsenceRequestStatus status, string? postComment, CancellationToken cancellationToken = default)
        {
            var absenceRequest = await _repository.GetByIdAsync(id);
            if (absenceRequest == null)
            {
                throw new Exception($"Absence request with ID {id} not found.");
            }

            absenceRequest.AbsenceRequestStatus = status;
            absenceRequest.PostComment = postComment;

            _repository.Update(absenceRequest);
            await _repository.SaveChangesAsync(cancellationToken);

            return absenceRequest.AbsenceRequestStatus;
        }
    }
}
