using SyncPoint365.Core.DTOs.Enums;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class EnumsService : IEnumsService
    {
        public IEnumerable<SelectItemDTO> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(item => new SelectItemDTO(Convert.ToInt32(item), item.ToString()))
                       .ToList();
        }
    }
}





