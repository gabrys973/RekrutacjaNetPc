using System.ComponentModel.DataAnnotations;

namespace Rekrutacja.Domain.Entities;

public class Category : Entity
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
}