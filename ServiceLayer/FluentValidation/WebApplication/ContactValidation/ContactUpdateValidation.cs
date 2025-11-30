using EntityLayer.WebApplication.ViewModels.ContactViewModels;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.ContactValidation
{
    public class ContactUpdateValidation : AbstractValidator<ContactUpdateVM>
    {
        public ContactUpdateValidation()
        {
            RuleFor(x => x.Location)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Location"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Location"))
                .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharacterAllowence("Location", 200));

            RuleFor(x => x.Email)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("Email", 100));

            RuleFor(x => x.Call)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Call"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Call"))
                .MaximumLength(13).WithMessage(ValidationMessages.MaximumCharacterAllowence("Call", 13));

            RuleFor(x => x.Map)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Map"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Map"));
        }
    }
}
