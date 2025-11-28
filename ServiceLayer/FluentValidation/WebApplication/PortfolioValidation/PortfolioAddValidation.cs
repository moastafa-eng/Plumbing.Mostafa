using EntityLayer.WebApplication.ViewModels.PortfolioViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.PortfolioValidation
{
    public class PortfolioAddValidation : AbstractValidator<PortfolioAddVM>
    {
        public PortfolioAddValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(200);

            RuleFor(x => x.FileName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.FileType)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Photo)
                .NotNull()
                .NotEmpty();
        }
    }
}
