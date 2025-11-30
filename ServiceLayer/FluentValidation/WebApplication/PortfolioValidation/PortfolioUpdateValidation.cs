using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.PortfolioValidation
{
    public class PortfolioUpdateValidation : AbstractValidator<PortfolioUpdateVM>
    {
        public PortfolioUpdateValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharacterAllowence("Title", 200));
        }
    }
}
