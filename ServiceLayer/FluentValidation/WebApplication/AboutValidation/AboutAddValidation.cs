using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using FluentValidation;
using ServiceLayer.Messages.WebApplication;

namespace ServiceLayer.FluentValidation.WebApplication.AboutValidation
{
    // This validator class inherits from AbstractValidator<T>
    // where T is the ViewModel to apply validation rules on.
    // The rules defined in the constructor are registered for AboutAddVM. 
    public class AboutAddValidation : AbstractValidator<AboutAddVM>
    {
        public AboutAddValidation()
        {
            RuleFor(x => x.Header)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Header"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Header"))
                .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharacterAllowence("Header", 200));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Description"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Description"))
                .MaximumLength(5000).WithMessage(ValidationMessages.MaximumCharacterAllowence("Description", 5000));


            RuleFor(x => x.Clients)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Clients"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Clients"))
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanMessage("Clients", 0))
                .LessThan(1000).WithMessage(ValidationMessages.LessThanMessage("Clients", 1000));


            RuleFor(x => x.Projects)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Projects"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Projects"))
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanMessage("Projects", 0))
                .LessThan(10000).WithMessage(ValidationMessages.LessThanMessage("Projects", 10000));


            RuleFor(x => x.HourOfSupport)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("HourOfSupport"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("HourOfSupport"))
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanMessage("HourOfSupport", 0))
                .LessThan(100000).WithMessage(ValidationMessages.LessThanMessage("HourOfSupport", 100000));


            RuleFor(x => x.HardWorkers)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("HardWorkers"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("HardWorkers"))
                .GreaterThan(0).WithMessage(ValidationMessages.GreaterThanMessage("HardWorkers", 0))
                .LessThan(99).WithMessage(ValidationMessages.LessThanMessage("HardWorkers", 99));

            RuleFor(x => x.Photo)
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Photo"))
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Photo"));

        }
    }
}
