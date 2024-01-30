using System.Collections.ObjectModel;

namespace ApiCatalogo.Models;
    public class Category
    {

    public Category()
    {
        Products = new Collection<Product>();
    } // Boa pratica inicializar a Collection
    public int CategoryId { get; set; }

    public string? Name { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Product>? Products { get; set; } // Relacionamento
}
