namespace SyncPoint365.Core.DTOs.AbsenceRequestTypes
{
    public class AbsenceRequestTypeUpdateDTO : BaseUpdateDTO
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
