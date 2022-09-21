using System.ComponentModel.DataAnnotations;
using Castle.Core.Internal;

namespace JustInTimeCompany.Models.ViewModels
{
    public class PilotEditFlightViewModel : IValidatableObject
    {
        public Guid FlightId { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? ScheduledArrival { get; set; }
        public DateTime? Arrival { get; set; }
        public bool WasLate { get; set; }

        [Display(Name = "Reason of the delay")]
        public string? DelayReason { get; set; } = "";


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(Arrival > ScheduledArrival)) yield break;
            WasLate = true;
            if (DelayReason.IsNullOrEmpty())
            {
                yield return new ValidationResult("The delay reason is mandatory for a late flight",
                    new[] { nameof(DelayReason) });
            }
        }
    }
}