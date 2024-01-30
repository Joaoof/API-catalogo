namespace ApiCatalogo.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public float Stock { get; set; }

        public DateTime DataRegister {  get; set; }

        public int CategoryId { get; set; } // Mapear para a coluna CategoryId de Category
        public Category? Category {  get; set; } // Relacionamento
    }
}
