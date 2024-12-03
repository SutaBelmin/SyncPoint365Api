using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.AbsenceRequests
{
    public class AbsenceRequestDTO : BaseDTO
    {
        public DateOnly DateFrom { get; set; } = default!;
        public DateOnly DateTo { get; set; } = default!;
        public DateOnly DateReturn { get; set; } = default!;
        public AbsenceRequestStatus Status { get; set; }
        public string? Comment { get; set; }
        public int AbsenceRequestTypeId { get; set; }
        public AbsenceRequestType AbsenceRequestType { get; set; } = default!;
        public int UserId { get; set; }
        public User User { get; set; } = default!;

    }
}
