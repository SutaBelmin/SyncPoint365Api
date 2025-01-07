using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.Entities
{
    public class AbsenceRequest : BaseEntity
    {
        public DateTime DateFrom { get; set; } = default!;
        public DateTime DateTo { get; set; } = default!;
        public DateTime DateReturn { get; set; } = default!;
        public AbsenceRequestStatus AbsenceRequestStatus { get; set; }
        public string? PreComment { get; set; }
        public string? PostComment { get; set; }
        public int AbsenceRequestTypeId { get; set; }
        public AbsenceRequestType AbsenceRequestType { get; set; } = default!;
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}