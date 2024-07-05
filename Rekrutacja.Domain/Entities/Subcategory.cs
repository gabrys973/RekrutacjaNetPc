using System.ComponentModel.DataAnnotations;

namespace Rekrutacja.Domain.Entities;

public class Subcategory : Entity
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }
}