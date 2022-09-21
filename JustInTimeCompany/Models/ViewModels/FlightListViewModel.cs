namespace JustInTimeCompany.Models.ViewModels
{
    public class FlightListViewModel
    {
        public List<Airport>? Airports { get; set; }
        public IEnumerable<Flight>? Flights { get; set; }
        public IEnumerable<UserMessage>? UserMessages { get; set; }

        public List<Flight> NextFlights()
        {
            if (Flights == null) return new List<Flight>();
            var nextFlights = Flights.ToList().FindAll(f => f.ScheduledDeparture > DateTime.Now);
            nextFlights.Sort((x, y) => x.ScheduledDeparture.CompareTo(y.ScheduledDeparture));
            return nextFlights;
        }
    }
}