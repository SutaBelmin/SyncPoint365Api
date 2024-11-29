using SyncPoint365.Core.DTOs.Enums;
using SyncPoint365.Core.Enums;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class EnumsService : IEnumsService
    {
        public IEnumerable<SimpleItemDTO> GetRoles()
        {
            return Enum.GetValues(typeof(Role))
                       .Cast<Role>()
                       .Select(role => new SimpleItemDTO((int)role, role.ToString()))
                       .ToList();
        }
    }
}





