using EntityLayer.WebApplication.ViewModels.ContactViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.ContactValidation
{
    public class ContactAddValidation : AbstractValidator<ContactAddVM>
    {
        public ContactAddValidation()
        {
            RuleFor(x => x.Location)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Call)
                .NotNull()
                .NotEmpty()
                .MaximumLength(13);

            RuleFor(x => x.Map)
                .NotNull()
                .NotEmpty();
                
        }
    }
}
