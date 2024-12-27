using SyncPoint365.Core.Enums;

namespace SyncPoint365.API.RESTModels
{
    public class AbsenceRequestsChangeStatusModel
    {

        public int Id { get; set; }
        public AbsenceRequestStatus NewStatus { get; set; }

    }
}
