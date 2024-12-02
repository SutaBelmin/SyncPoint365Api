namespace SyncPoint365.Core.DTOs.Enums
{
    public class SelectItemDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }


        public SelectItemDTO(int id, string label)
        {
            Id = id;
            Label = label;
        }

    }
}
