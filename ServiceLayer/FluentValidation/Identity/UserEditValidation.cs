using EntityLayer.Identity.ViewModels;
using FluentValidation;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.Identity
{
    public class UserEditValidation : AbstractValidator<UserEditVM>
    {
        public UserEditValidation()
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Username"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Username"));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Password"));

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage(IdentityMessages.ComparePassword());

        }
    }
}
