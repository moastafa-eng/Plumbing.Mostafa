using EntityLayer.WebApplication.ViewModels.CategoryViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.CategoryValidation
{
    public class CategoryAddValidation : AbstractValidator<CategoryAddVM>
    {
        public CategoryAddValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
        }
    }
}
