using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(20, ErrorMessage = "The name must be between {5} and {20} characters", MinimumLength = 5)]
        public string? Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The description must max {1} characters", MinimumLength = 5)]
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
    }
}
