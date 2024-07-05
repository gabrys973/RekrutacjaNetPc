using Rekrutacja.Application.Dtos.Subcategories;

namespace Rekrutacja.Application.Dtos.Categories;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SubcategoryDto> Subcategories { get; set; } = new List<SubcategoryDto>();
}