using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.AbsenceRequests
{
    public class AbsenceRequestUpdateDTO : BaseUpdateDTO
    {
        public DateTime DateFrom { get; set; } = default!;
        public DateTime DateTo { get; set; } = default!;
        public DateTime DateReturn { get; set; } = default!;
        public AbsenceRequestStatus AbsenceRequestStatus { get; set; }
        public string? PreComment { get; set; }
        public string? PostComment { get; set; }
        public int AbsenceRequestTypeId { get; set; }
        public int UserId { get; set; }
    }
}
