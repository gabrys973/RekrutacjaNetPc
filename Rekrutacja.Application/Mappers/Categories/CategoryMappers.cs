using Rekrutacja.Application.Dtos.Categories;
using Rekrutacja.Application.Mappers.Subcategories;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Application.Mappers.Categories;

public static class CategoryMappers
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };

        foreach(var subcategory in category.Subcategories)
        {
            categoryDto.Subcategories.Add(subcategory.ToSubcategoryDto());
        }

        return categoryDto;
    }
}