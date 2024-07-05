using System.ComponentModel.DataAnnotations;

namespace Rekrutacja.Domain;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}