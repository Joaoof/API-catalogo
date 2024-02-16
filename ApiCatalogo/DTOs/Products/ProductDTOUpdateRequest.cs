using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs.Products
{
    public class ProductDTOUpdateRequest : IValidatableObject
    {
        [Range(1, 9999, ErrorMessage = "The stock must be between 1 and 9999")]
        public float Stock { get; set; }

        public DateTime DataRegister { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataRegister.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("The Date must be greater than the current Date", new[] { nameof(this.DataRegister)});
            }
        }
    }
}
