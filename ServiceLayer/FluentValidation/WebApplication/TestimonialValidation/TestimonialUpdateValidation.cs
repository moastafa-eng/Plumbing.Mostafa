using EntityLayer.WebApplication.ViewModels.TestimonialViewModels;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.TestimonialValidation
{
    public class TestimonialUpdateValidation : AbstractValidator<TestimonialUpdateVM>
    {
        public TestimonialUpdateValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("FullName"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("FullName"))
                .MaximumLength(100).WithMessage(ValidationMessages.MaximumCharacterAllowence("FullName", 100));

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Title"))
                .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharacterAllowence("Title", 200));

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Comment"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Comment"))
                .MaximumLength(2000).WithMessage(ValidationMessages.MaximumCharacterAllowence("Comment", 2000));
        }
    }
}
