using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Rekrutacja.Client.Validators;

public class PasswordAttribute : ValidationAttribute
{
    private List<string> patterns = new List<string> {
            @"[a-z]",          // mała litera
            @"[A-Z]",          // wielka litera
            @"[0-9]",          // cyfra
            @"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\]]" // znak specjalny
        };

    public PasswordAttribute()
    { }

    public string GetErrorMessage() => "Hasło musi zawierać małą literę, wielką literę, cyfrę oraz znak specjalny";

    // walidator sprawdza, czy hasło posiada wartość z każdego patternu
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = (string)value;
        var counter = 0;

        foreach(var p in patterns)
        {
            if(Regex.IsMatch(password, p))
            {
                counter++;
            }
        }

        if(counter < 4)
            return new ValidationResult(GetErrorMessage());
        else
            return ValidationResult.Success;
    }
}