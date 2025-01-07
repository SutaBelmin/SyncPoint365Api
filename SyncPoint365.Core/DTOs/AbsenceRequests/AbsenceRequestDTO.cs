using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.AbsenceRequests
{
    public class AbsenceRequestDTO : BaseDTO
    {
        public DateTime DateFrom { get; set; } = default!;
        public DateTime DateTo { get; set; } = default!;
        public DateTime DateReturn { get; set; } = default!;
        public AbsenceRequestStatus AbsenceRequestStatus { get; set; }
        public string? PreComment { get; set; }
        public string? PostComment { get; set; }
        public AbsenceRequestType AbsenceRequestType { get; set; } = default!;
        public int AbsenceRequestTypeId { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; } = default!;

    }
}
