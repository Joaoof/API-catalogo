using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCatalogo.Validations;

namespace ApiCatalogo.Models
{
    [Table("Products")]
    public class Product : IValidatableObject
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(20, ErrorMessage = "The name must be between {5} and {20} characters", MinimumLength = 5)]
        public string? Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The description must max {1} characters", MinimumLength = 5)]
        //[FirstCapitalLetter]
        public string? Description { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "The price must be between {1} and {2}")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }  

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string? ImageUrl { get; set; }

        public float Stock { get; set; }

        public DateTime DataRegister {  get; set; }

        public int CategoryId { get; set; } // Mapear para a coluna CategoryId de Category

        [JsonIgnore] //ignorada na serelização
        public Category? Category {  get; set; } // Relacionamento

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                var firstletter = this.Name[0].ToString();
                if (firstletter != firstletter.ToUpper())
                {
                    yield return new ValidationResult("the first letter must be capitalized", new[] { nameof(this.Name) }
                    );
    
                }
            }

            if (this.Stock <= 0)
            {
                yield return new ValidationResult("the stock must be greater than zero", new[] { nameof(this.Stock) });
            }
        }
    }
}
