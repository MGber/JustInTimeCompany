using System.ComponentModel.DataAnnotations;
using JustInTimeCompany.Validations;

namespace JustInTimeCompany.Models.ViewModels
{
    public class FlightViewModel
    {
        public Flight? Flight { get; set; }
        public List<Airport> Airports { get; set; }
        public List<Helicopter> Helicopters { get; set; }
        public List<Flight> Flights { get; set; }

        public IList<ApplicationUser> Pilots { get; set; }


        public bool PilotCanFly()
        {
            Flight.SetScheduledArrival();
            return !(Flights.Any(
                f => f.Pilot == Flight.Pilot &&
                    IsBetween(Flight.ScheduledDeparture, f.ScheduledDeparture, f.ScheduledArrival) || Flights.Any(
                        f => f.Pilot == Flight.Pilot && IsBetween(Flight.ScheduledArrival, f.ScheduledDeparture,
                            f.ScheduledArrival))));
        }

        public static bool IsBetween(DateTime? input, DateTime date1, DateTime? date2)
        {
            return (input >= date1 && input <= date2);
        }

        public bool PilotsAreSame(ApplicationUser pilot)
        {
            return false;
        }

        public List<ApplicationUser> PilotsForDate(DateTime date)
        {
            var pilotList = Pilots.ToList();
            foreach (var flight in Flights)
            {
                if (IsBetween(date, flight.ScheduledDeparture, flight.ScheduledArrival))
                    if (!PilotsAreSame(flight.Pilot))
                        pilotList.Remove(flight.Pilot);
            }

            return pilotList;
        }
    }
}