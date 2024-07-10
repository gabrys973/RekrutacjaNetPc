using Rekrutacja.Client.Validators;
using System.ComponentModel.DataAnnotations;

namespace Rekrutacja.Client.Models.Requests.Contacts;

[Serializable]
public class ContactRequestModel
{
    [Required(ErrorMessage = "Pole jest wymagane")]
    [MaxLength(250, ErrorMessage = "Maksymalna długość znaków to 250")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [MaxLength(250, ErrorMessage = "Maksymalna długość znaków to 250")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [MaxLength(250, ErrorMessage = "Maksymalna długość znaków to 250"), EmailAddress(ErrorMessage = "Błędny format adresu email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [MinLength(8, ErrorMessage = "Minimalna długość znaków to 8")]
    [Password]
    public string Password { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [MaxLength(20, ErrorMessage = "Maksymalna długość znaków to 20"), Phone(ErrorMessage = "Błędny format numeru telefonu")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [ToToday]
    public DateTime DateOfBirth { get; set; } = DateTime.Today.AddDays(-1);

    [Required(ErrorMessage = "Pole jest wymagane")]
    public int CategoryId { get; set; } = 1;

    public int? SubcategoryId { get; set; }

    [MaxLength(250, ErrorMessage = "Maksymalna długość znaków to 250")]
    public string CustomSubcategory { get; set; }
}