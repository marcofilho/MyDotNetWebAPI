using DevIO.Business.Models.Validations.Documents;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("The field {PropertyName} must be provided")
                .Length(2, 100)
                .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

            When(f => f.Type == SupplierType.Individual, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CpfValidation.CpfLength)
                    .WithMessage("The field {PropertyName} must have {ComparisonValue} characters, but {PropertyValue} was provided.");
                RuleFor(f => CpfValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("The provided document is invalid.");
            });

            When(f => f.Type == SupplierType.LegalEntity, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CnpjValidation.CnpjLength)
                    .WithMessage("The field Document must have {ComparisonValue} characters, but {PropertyValue} was provided.");
                RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("The provided document is invalid.");
            });
        }
    }
}
