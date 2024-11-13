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
        public AbsenceRequestTypesService(IAbsenceRequestTypesRepository repository,
            IMapper mapper,
            IValidator<AbsenceRequestTypeAddDTO> addValidator,
            IValidator<AbsenceRequestTypeUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;

        }
    }
}
