using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.Entities;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceRequestTypeController : BaseController<AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>
    {
        protected readonly IAbsenceRequestTypeService _absenceRequestTypeService;
        public AbsenceRequestTypeController(IAbsenceRequestTypeService service) : base(service)
        {
            _absenceRequestTypeService = service;
        }
    }
}
