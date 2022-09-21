using System.ComponentModel.DataAnnotations;
using JustInTimeCompany.Models.Utils;

namespace JustInTimeCompany.Models
{
    //TODO quand tu set l'heure de départ, ca set l'heure d'arrivée
    public class Flight : IValidatableObject
    {
        private DateTime _scheduledDeparture;
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Departure airport is mandatory")]
        [Display(Name = "Departure airport")]
        public virtual Airport? From { get; set; }

        [Required(ErrorMessage = "Arrival airport is mandatory")]
        [Display(Name = "Arrival airport")]
        public virtual Airport? To { get; set; }

        [Required(ErrorMessage = "Flight helicopter is mandatory")]
        [Display(Name = "Flight helicopter")]
        public virtual Helicopter Helicopter { get; set; }

        [Required(ErrorMessage = "Pilot is mandatory")]
        [Display(Name = "Flight pilot")]
        public virtual ApplicationUser? Pilot { get; set; }

        [Required(ErrorMessage = "Scheduled departure is mandatory")]
        [Display(Name = "Departure")]
        public virtual DateTime ScheduledDeparture
        {
            get => _scheduledDeparture;
            set
            {
                _scheduledDeparture = value;
                // + 15 min for landing
                ScheduledArrival = value.AddHours(DistanceKm() / Helicopter.Speed).AddMinutes(15);
            }
        } // Mail when changed.

        public DateTime? RealDeparture { get; set; }
        public DateTime? ScheduledArrival { get; set; }

        public DateTime? RealArrival { get; set; }
        public virtual List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public bool WasLate { get; set; } = false;
        [Display(Name = "Delay reason")] public string? DelayReason { get; set; } = "";

        public string GetSeatCount()
        {
            return "" + Reservations.Select(r => r.SeatAmount).DefaultIfEmpty(0).Sum() + "/" +
                   Helicopter.SeatCount;
        }

        public string GetPilotFullName()
        {
            return Pilot.FirstName + " " + Pilot.LastName;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ScheduledDeparture < DateTime.Now)
                yield return new ValidationResult("Departure time cannot be in the past",
                    new[] { nameof(Flight) + "." + nameof(ScheduledDeparture) });
            if (From.Id == To.Id)
                yield return new ValidationResult("Departure airport cannot be the same as arrival airport.",
                    new[]
                    {
                        nameof(Flight) + "." + nameof(From),
                        nameof(Flight) + "." + nameof(To)
                    });
            if (RealDeparture != null && RealArrival != null)
            {
                if (RealDeparture >= RealArrival)
                    yield return new ValidationResult("Departure cannot be after arrival buh time goes in one way.",
                        new[]
                        {
                            nameof(Flight) + "." + nameof(RealDeparture), nameof(Flight) + "." + nameof(RealArrival)
                        });
                if (RealDeparture < DateTime.Now)
                    yield return new ValidationResult("Departure time must be greater than now",
                        new[] { nameof(RealDeparture) });
                if (RealArrival < DateTime.Now)
                    yield return new ValidationResult("Departure time must be greater than now",
                        new[] { nameof(RealArrival) });
            }

            yield return ValidationResult.Success!;
        }

        public Guid? GetReservationId(ApplicationUser user) =>
            Reservations.SingleOrDefault(r => r.User.Id.Equals(user.Id))?.Id;

        public int GetFreeSeats()
        {
            return Helicopter.SeatCount - Reservations.Select(r => r.SeatAmount).Sum();
        }

        public int GetOccupiedSeats => Reservations.Select(r => r.SeatAmount).Sum();

        public bool AddReservation(Reservation reservation)
        {
            if (reservation.SeatAmount > GetFreeSeats()) return false;
            Reservations.Add(reservation);
            return true;
        }

        public bool ChangeReservation(Reservation reservation, int newSeatAmount)
        {
            var oldReservation = Reservations.SingleOrDefault(r => r.Id == reservation.Id);
            int freeSeats = GetFreeSeats() + oldReservation!.SeatAmount;
            if (freeSeats < newSeatAmount) return false;
            reservation.SeatAmount = newSeatAmount;
            return true;
        }

        public bool RemoveReservation(Reservation reservation)
        {
            var departureMinus24h = ScheduledDeparture.AddDays(-1);
            if (DateTime.Now > departureMinus24h)
                return false;
            Reservations.Remove(reservation);
            return true;
        }

        public Reservation? ForUser(ApplicationUser user) => Reservations.SingleOrDefault(r => r.User.Id == user.Id);

        public double DistanceKm()
        {
            return JitcUtil.Distance(From.Latitude, From.Longitude, To.Latitude, To.Longitude);
        }


        public bool HaveAlreadyBooked(ApplicationUser user) => Reservations.Any(r => r.User.Id.Equals(user.Id));

        public void SetScheduledArrival()
        {
            ScheduledArrival = ScheduledDeparture.AddHours(DistanceKm() / Helicopter.Speed).AddMinutes(15);
        }
    }
}

//TODO pas plus de sieges que de places.