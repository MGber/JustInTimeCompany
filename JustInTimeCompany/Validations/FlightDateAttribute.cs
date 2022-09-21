using System.ComponentModel.DataAnnotations;
using JustInTimeCompany.Models;

namespace JustInTimeCompany.Validations
{
    public class FlightDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var a = validationContext.ObjectInstance;
            if (value == null)
                return new ValidationResult("An error occured while trying to add the new Flight.");
            return value is Flight model && model.ScheduledArrival <= model.ScheduledDeparture
                ? new ValidationResult("Arrival date must be greater than departure date.")
                : ValidationResult.Success;
        }
    }
}