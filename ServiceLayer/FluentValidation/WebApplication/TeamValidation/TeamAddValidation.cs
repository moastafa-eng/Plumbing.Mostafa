using EntityLayer.WebApplication.ViewModels.TeamViewModels;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.TeamValidation
{
    public class TeamAddValidation : AbstractValidator<TeamAddVM>
    {
        public TeamAddValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("FullName"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("FullName"))
                .MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("FullName", 100));

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharacterAllowence("Title", 200));

            RuleFor(x => x.Photo)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Photo"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Photo"));
        }
    }
}
