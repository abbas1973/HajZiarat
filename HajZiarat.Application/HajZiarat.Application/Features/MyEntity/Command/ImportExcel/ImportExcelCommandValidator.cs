using Application.Repositories;
using Application.Validators;
using FluentValidation;
using Resources;

namespace Application.Features.MyEntities
{
    public class ImportExcelCommandValidator : AbstractValidator<ImportExcelCommand>
    {
        public ImportExcelCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.ExcelFile)
               .NotNull().WithMessage(ValidatorErrors.Required)
               .Must(BaseValidator.BeAValidExcel).WithMessage("{PropertyName} باید اکسل باشد!")
               .MustAsync(async (model, field, cancellation) =>
               {
                   if (field == null)
                       return true;
                   return field.Length > 0 && field.Length < 52428487; // کمتر از 50 مگابایت باشد
               }).WithMessage("{PropertyName} کمتر از 50 مگابایت باشد!");

        }
    }
}
