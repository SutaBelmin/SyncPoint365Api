namespace SyncPoint365.Core.DTOs.Enums
{
    public class SimpleItemDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }


        public SimpleItemDTO(int id, string label)
        {
            Id = id;
            Label = label;
        }

    }
}
