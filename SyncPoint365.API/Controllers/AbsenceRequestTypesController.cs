using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AbsenceRequestTypesController : BaseController<AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>
    {
        protected readonly IAbsenceRequestTypesService _absenceRequestTypeService;
        public AbsenceRequestTypesController(IAbsenceRequestTypesService service) : base(service)
        {
            _absenceRequestTypeService = service;
        }
    }
}
