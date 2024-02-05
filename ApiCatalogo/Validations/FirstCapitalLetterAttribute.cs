using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Validations
{
    public class FirstCapitalLetterAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstletter = value.ToString()[0].ToString();
            if (firstletter != firstletter.ToUpper())
            {
                return new ValidationResult("the first letter of your name must be capitalized");
            }

            return ValidationResult.Success; 
        }
    }
}
