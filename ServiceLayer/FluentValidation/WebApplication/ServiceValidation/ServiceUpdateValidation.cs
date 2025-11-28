using EntityLayer.WebApplication.ViewModels.ServiceViewModels;
using FluentValidation;

namespace ServiceLayer.FluentValidation.WebApplication.ServiceValidation
{
    public class ServiceUpdateValidation : AbstractValidator<ServiceUpdateVM>
    {
        public ServiceUpdateValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(2000);

            RuleFor(x => x.Icon)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
        }
    }
}
