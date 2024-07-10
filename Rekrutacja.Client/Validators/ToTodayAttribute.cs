using System.ComponentModel.DataAnnotations;

namespace Rekrutacja.Client.Validators;

public class ToTodayAttribute : ValidationAttribute
{
    public ToTodayAttribute()
    { }

    public string GetErrorMessage() => "Data musi pochodzić z przeszłości";

    //walidator sprawdza, czy data jest mniejsza od daty dzisiejszej
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var date = (DateTime)value;

        if(DateTime.Compare(date, DateTime.Today) >= 0)
            return new ValidationResult(GetErrorMessage());
        else
            return ValidationResult.Success;
    }
}