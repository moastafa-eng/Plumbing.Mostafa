using EntityLayer.WebApplication.ViewModels.AboutViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.AboutValidation
{
    public class AboutUpdateValidation : AbstractValidator<AboutUpdateVM>
    {
        public AboutUpdateValidation()
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
        }
    }
}
