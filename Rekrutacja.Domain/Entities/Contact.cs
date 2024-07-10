using System.ComponentModel.DataAnnotations;

namespace Rekrutacja.Domain.Entities;

public class Contact : Entity
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Surname { get; set; }

    [Required]
    [MaxLength(250)]
    public string Email { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Password { get; set; }

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int? SubcategoryId { get; set; }
    public Subcategory? Subcategory { get; set; }

    [MaxLength(250)]
    public string? CustomSubcategory { get; set; } = string.Empty;
}