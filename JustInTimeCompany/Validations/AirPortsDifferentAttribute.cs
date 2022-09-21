using System.ComponentModel.DataAnnotations;
using JustInTimeCompany.Models;
using JustInTimeCompany.Models.ViewModels;

namespace JustInTimeCompany.Validations
{
    public class AirPortsDifferentAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var viewModel = (ManyFlightsViewModel)validationContext.ObjectInstance;
            if (viewModel.From.Equals(viewModel.To))
                return new ValidationResult("Departure and arrival airports must be different.",
                    new[] { nameof(viewModel.From), nameof(viewModel.To) });
            return ValidationResult.Success;
        }
    }
}