using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Service.Services
{
    public class AbsenceRequestTypeService : BaseService<AbsenceRequestType, AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>, IAbsenceRequestTypeService
    {
        private readonly IAbsenceRequestTypeRepository _repository;
        public AbsenceRequestTypeService(IAbsenceRequestTypeRepository repository, 
            IMapper mapper,
            IValidator<AbsenceRequestTypeAddDTO> addValidator,
            IValidator<AbsenceRequestTypeUpdateDTO> updateValidator) 
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
        
        }
    }
}
