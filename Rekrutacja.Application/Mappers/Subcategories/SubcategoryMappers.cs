using Rekrutacja.Application.Dtos.Subcategories;
using Rekrutacja.Domain.Entities;

namespace Rekrutacja.Application.Mappers.Subcategories;

public static class SubcategoryMappers
{
    public static SubcategoryDto ToSubcategoryDto(this Subcategory subCategory)
    {
        return new SubcategoryDto(subCategory.Id, subCategory.Name);
    }
}