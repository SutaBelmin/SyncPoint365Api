using SyncPoint365.Core.DTOs.Enums;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IEnumsService
    {
        IEnumerable<SelectItemDTO> GetEnumValues<T>() where T : Enum;
    }
}
