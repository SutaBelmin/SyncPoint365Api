using SyncPoint365.Core.Enums;

namespace SyncPoint365.API.RESTModels
{
    public class AbsenceRequestStatusChangeModel
    {

        public int Id { get; set; }
        public AbsenceRequestStatus Status { get; set; }
        public string? PostComment { get; set; }
    }
}
