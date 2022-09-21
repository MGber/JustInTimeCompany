using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using JustInTimeCompany.Models.Factory;
using JustInTimeCompany.Validations;

namespace JustInTimeCompany.Models.ViewModels
{
    public class ManyFlightsViewModel : IValidatableObject
    {
        [Required] public Airport From { get; set; }
        [Required] public Airport? To { get; set; }
        [Required] public Helicopter? Helicopter { get; set; }
        [Required] public ApplicationUser Pilot { get; set; }

        [DataType(DataType.Time)]
        [TimeSpanValidator(MinValueString = "0:0:0", MaxValueString = "0:23:59")]
        public TimeSpan HM { get; set; }

        [Required] public List<int> DayNumbers { get; set; }
        [Required] public DateTime EndDate { get; set; } = DateTime.Now;

        public List<DayOfWeek> GetDays()
        {
            var days = new List<DayOfWeek>();
            DayNumbers.ForEach(d =>
            {
                if (d is >= 0 and < 7)
                    days.Add(Convert(d));
            });
            days.RemoveAll(day => day == null);
            return days;
        }

        private static DayOfWeek Convert(int dayNumber)
        {
            return dayNumber switch
            {
                0 => DayOfWeek.Monday,
                1 => DayOfWeek.Tuesday,
                2 => DayOfWeek.Wednesday,
                3 => DayOfWeek.Thursday,
                4 => DayOfWeek.Friday,
                5 => DayOfWeek.Saturday,
                6 => DayOfWeek.Sunday,
                _ => throw new ArgumentOutOfRangeException(nameof(dayNumber), dayNumber, null)
            };
        }

        public List<Flight> GetFlights()
        {
            var flights = new List<Flight>();
            var departures = new List<DateTime>();
            var currentDate = DateTime.Now.Date;
            while (currentDate <= EndDate)
            {
                var days = GetDays();
                if (days.Contains(currentDate.DayOfWeek))
                {
                    var flight = new Flight()
                    {
                        Id = Guid.NewGuid(),
                        From = From,
                        To = To,
                        Helicopter = Helicopter,
                        Pilot = Pilot,
                        ScheduledDeparture = currentDate.Date.AddHours(HM.Hours).AddMinutes(HM.Minutes),
                    };
                    flights.Add(flight);
                }

                currentDate = currentDate.AddDays(1);
            }

            return flights;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (From.Equals(To))
                yield return new ValidationResult("Departure and arrival airports cannot be the same",
                    new[] { nameof(From), nameof(To) });
            if (DayNumbers.Count(d => d > 6 || d < 0) > 0)
                yield return new ValidationResult("It seems you selected days out of the range",
                    new[] { nameof(DayNumbers) });
        }
    }
}