using EntityLayer.WebApplication.ViewModels.HomePageViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.HomepageValidation
{
    public class HomePageAddValidation : AbstractValidator<HomePageAddVM>
    {
        public HomePageAddValidation()
        {
            RuleFor(x => x.Header)
                .NotEmpty()
                .NotNull()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(2000);

            RuleFor(x => x.VideoLink)
                .NotEmpty()
                .NotNull();
        }
    }
}
