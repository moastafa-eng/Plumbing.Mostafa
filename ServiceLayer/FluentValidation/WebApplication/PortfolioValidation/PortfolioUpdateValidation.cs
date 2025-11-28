using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.PortfolioValidation
{
    public class PortfolioUpdateValidation : AbstractValidator<PortfolioUpdateVM>
    {
        public PortfolioUpdateValidation()
        {
            RuleFor(x => x.FileName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.FileType)
                .NotEmpty()
                .NotNull();
        }
    }
}
