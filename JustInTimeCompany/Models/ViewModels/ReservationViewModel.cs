using System.ComponentModel.DataAnnotations;

namespace JustInTimeCompany.Models.ViewModels
{
    public class ReservationViewModel : IValidatableObject
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cannot book less than 1 seat for a flight.")]
        public int SeatAmount { get; set; }

        public Guid? FlightId { get; set; }

        public Guid? ReservationId { get; set; }

        public Flight? Flight { get; set; }

        public Reservation? Reservation { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Flight == null)
                yield return new ValidationResult("An error has occurred. Cannot find the flight.");
            else
            {
                if (Flight.ScheduledDeparture < DateTime.Now)
                    yield return new ValidationResult("Cannot modify a past reservation.",
                        new[] { nameof(SeatAmount) });
                var reservation = Flight.Reservations.SingleOrDefault(r => r.Id == ReservationId);
                if (reservation != null)
                {
                    if (Flight.GetFreeSeats() + reservation.SeatAmount < SeatAmount)
                        yield return new ValidationResult(
                            $"Cannot exceed seat limit for flight. {Flight.GetFreeSeats() + reservation.SeatAmount} free seats.",
                            new[] { nameof(SeatAmount) });
                }
                else
                {
                    if (Flight.GetFreeSeats() < SeatAmount)
                        yield return new ValidationResult(
                            $"Cannot exceed seat limit for flight. {Flight.GetFreeSeats()} free seats.",
                            new[] { nameof(SeatAmount) });
                }
            }
        }
    }
}