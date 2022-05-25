namespace TimetableApi.Models
{
    public class TimetableItemDTO
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Movie { get; set; }
        
        public string? Cinema { get; set; }        
    }
}