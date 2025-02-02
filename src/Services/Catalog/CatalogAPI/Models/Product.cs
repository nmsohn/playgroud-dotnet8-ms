namespace CatalogAPI.Models;

public class Product : Model
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Categories { get; set; } = new();
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
}