namespace JustInTimeCompany.Models.ViewModels
{
    public class ReservationListViewModel
    {
        public ReservationListViewModel(IEnumerable<Flight> flights, ApplicationUser user)
        {
            FlightReservations = new List<KeyValuePair<Flight, Reservation?>>();
            foreach (var flight in flights)
            {
                if (flight.HaveAlreadyBooked(user))
                    FlightReservations.Add(new KeyValuePair<Flight, Reservation?>(flight, flight.ForUser(user)));
            }
        }

        public List<KeyValuePair<Flight, Reservation?>> FlightReservations { get; set; }
    }
}