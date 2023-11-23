using Application.DTOs;
using FluentValidation;
using Resources;

namespace Application.Validators
{
    public class IBaseEntityDTOValidator : AbstractValidator<IBaseEntityDTO>
    {
        public IBaseEntityDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(string.Format(Messages.ErrorRequired, "{PropertyName}"))
                .GreaterThan(0).WithMessage(string.Format(Messages.ErrorRequired, "{PropertyName}", "{ComparisonValue}"));
        }
    }
}
