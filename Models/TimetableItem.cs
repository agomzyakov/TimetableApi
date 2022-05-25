namespace TimetableApi.Models
{
    public class TimetableItem
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public long MovieId { get; set; }
        public long CinemaId { get; set; }        
    }
}