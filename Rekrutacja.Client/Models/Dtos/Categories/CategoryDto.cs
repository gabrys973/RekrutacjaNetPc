using Rekrutacja.Client.Models.Dtos.Subcategories;

namespace Rekrutacja.Client.Models.Dtos.Categories;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SubcategoryDto> Subcategories { get; set; } = new();
}