using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.TestimonialValidation
{
    public class TestimonialUpdateValidation : AbstractValidator<TestimonialUpdateVM>
    {
        public TestimonialUpdateValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

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

            RuleFor(x => x.Comment)
                .NotEmpty()
                .NotNull()
                .MaximumLength(2000);
        }
    }
}
