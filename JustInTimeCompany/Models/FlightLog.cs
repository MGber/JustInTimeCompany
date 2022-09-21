namespace JustInTimeCompany.Models
{
    public class FlightLog
    {
        public Guid Id { get; set; }
        public virtual Flight Flight { get; set; }
        public DateTime Date { get; set; }
        public string OldJson { get; set; }
        public string NewJson { get; set; }
    }
}