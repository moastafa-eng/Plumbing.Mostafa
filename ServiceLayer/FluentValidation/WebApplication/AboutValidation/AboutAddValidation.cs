using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using FluentValidation;

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
                .NotEmpty()
                .NotNull()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(5000);


            RuleFor(x => x.Clients)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(1000);


            RuleFor(x => x.Projects)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(10000);


            RuleFor(x => x.HourOfSupport)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(100000);


            RuleFor(x => x.HardWorkers)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(99);

            RuleFor(x => x.Photo)
                .NotNull()
                .NotEmpty();

        }
    }
}
