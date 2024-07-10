using FluentValidation;
using Rekrutacja.Application.Requests.Contacts;

namespace Rekrutacja.Application.Validators.Contacts;

public class ContactRequestValidator : AbstractValidator<ContactRequest>
{
    public ContactRequestValidator()
    {
        RuleFor(contact => contact.Name).NotEmpty().MaximumLength(250);
        RuleFor(contact => contact.Surname).NotEmpty().MaximumLength(250);
        RuleFor(contact => contact.Email).NotEmpty().MaximumLength(250).EmailAddress();
        RuleFor(contact => contact.Password).SetValidator(new PassWordValidator());
        RuleFor(contact => contact.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(contact => contact.DateOfBirth).NotEmpty().LessThan(DateTime.Today);
        RuleFor(contact => contact.CategoryId).NotEmpty();
        // podkategoria powinna być możliwa do wyboru tylko wtedy, kiedy id kategorii = 1 (Służbowy)
        RuleFor(contact => contact.SubcategoryId).Empty().When(contact => contact.CategoryId != 1).WithMessage("Subcategory Id must be empty when Category Id different than 1");
        // podkategoria wpisywana ręcznie powinna być możliwa do wyboru tylko wtedy, kiedy id kategorii = 3 (Inny)
        RuleFor(contact => contact.CustomSubcategory).Empty().When(contact => contact.CategoryId != 3).WithMessage("Custom Subcategory must be empty when Category Id different than 3").MaximumLength(250);
    }

    private class PassWordValidator : AbstractValidator<string>
    {
        // walidator haseł
        public PassWordValidator()
        {
            RuleFor(p => p).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\]]").WithMessage("Your password must contain at least one special character.");
        }
    }
}