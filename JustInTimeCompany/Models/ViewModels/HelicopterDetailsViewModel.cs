namespace JustInTimeCompany.Models.ViewModels
{
    public class HelicopterDetailsViewModel
    {
        public Guid HelicopterId { get; set; }
        public Helicopter? Helicopter { get; set; }
        public Guid FlightId { get; set; }
    }
}
