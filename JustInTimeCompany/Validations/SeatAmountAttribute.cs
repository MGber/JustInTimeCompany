using System.ComponentModel.DataAnnotations;
using JustInTimeCompany.Models;

namespace JustInTimeCompany.Validations
{
    public class SeatAmountAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}