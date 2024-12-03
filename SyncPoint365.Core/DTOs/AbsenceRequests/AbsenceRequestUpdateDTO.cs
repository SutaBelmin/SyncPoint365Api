using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.AbsenceRequests
{
    public class AbsenceRequestUpdateDTO : BaseUpdateDTO
    {
        public DateOnly DateFrom { get; set; } = default!;
        public DateOnly DateTo { get; set; } = default!;
        public DateOnly DateReturn { get; set; } = default!;
        public AbsenceRequestStatus AbsenceRequestStatus { get; set; }
        public string Comment { get; set; } = default!;
        public int AbsenceRequestTypeId { get; set; }
    }
}
