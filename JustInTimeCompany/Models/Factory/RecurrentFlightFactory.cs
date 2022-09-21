namespace JustInTimeCompany.Models.Factory
{
    //TODO canBeArrived (Flight)
    public class RecurrentFlightFactory
    {
        public List<Flight> GetFlights(Airport from, Airport to, Helicopter helicopter, ApplicationUser pilot,
            TimeSpan hm, List<DayOfWeek> days, DateTime endDate)
        {
            var flights = new List<Flight>();
            var departures = new List<DateTime>();
            var currentDate = DateTime.Now.Date;
            while (currentDate <= endDate)
            {
                if (days.Contains(currentDate.DayOfWeek))
                {
                    var flight = new Flight()
                    {
                        Id = Guid.NewGuid(),
                        From = from,
                        To = to,
                        Helicopter = helicopter,
                        Pilot = pilot,
                        ScheduledDeparture = currentDate.Date.AddHours(hm.Hours).AddMinutes(hm.Minutes),
                    };
                    flight.SetScheduledArrival();
                }

                currentDate = currentDate.AddDays(1);
            }

            return flights;
        }
    }
}