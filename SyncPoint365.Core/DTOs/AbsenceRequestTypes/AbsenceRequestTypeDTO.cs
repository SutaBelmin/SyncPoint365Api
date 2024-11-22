namespace SyncPoint365.Core.DTOs.AbsenceRequestTypes
{
    public class AbsenceRequestTypeDTO : BaseDTO
    {
        public string Name { get; set; } = default!;
        public bool? IsActive { get; set; }
    }
}
