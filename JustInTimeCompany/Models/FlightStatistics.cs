namespace JustInTimeCompany.Models
{
    public class FlightStatistics
    {
        public FlightStatistics(IList<Flight> flights, IList<Helicopter> helicopters)
        {
            Helicopters = helicopters;
            Flights = flights;
            SetDelayedFlights();
        }

        public IList<Flight> Flights { get; set; }
        public IList<Flight> DelayedFlights { get; set; }
        public IList<Helicopter> Helicopters { get; set; }

        public double FillingRatePercent(Helicopter helicopter)
        {
            var flightForHelicopter = Flights!.Where(f => f.Helicopter.Id.Equals(helicopter.Id));
            var filledSeats = 0.0;
            var totalSeats = 0.0;
            flightForHelicopter.ToList().ForEach(f =>
            {
                filledSeats += f.GetOccupiedSeats;
                totalSeats += f.Helicopter.SeatCount;
            });
            if (filledSeats == 0.0) return 0.0;
            return filledSeats / totalSeats * 100;
        }

        public List<KeyValuePair<string, double>> FillingRates()
        {
            if (Helicopters != null)
                return Helicopters.Select(distinctHelicopter =>
                        new KeyValuePair<string, double>(distinctHelicopter.Model,
                            FillingRatePercent(distinctHelicopter)))
                    .ToList();
            return new List<KeyValuePair<string, double>>();
        }

        public int DelayedFlightsCount => DelayedFlights.Count;

        public void SetDelayedFlights()
        {
            DelayedFlights = Flights.Where(f => f.WasLate || (f.RealArrival > f.ScheduledArrival)).ToList();
        }
    }
}