using Infrastructure.CrossCutting.Validation.Interfaces.Specification;
using Infrastructure.CrossCutting.Validation.Interfaces.Validation;

namespace Infrastructure.CrossCutting.Validation
{
    public class ValidationRule<TEntity> : IValidationRule<TEntity>
    {
        private readonly ISpecification<TEntity> _specificationRule;

        public ValidationRule(ISpecification<TEntity> specificationRule, string errorField, string errorMessage)
        {
            _specificationRule = specificationRule;
            ErrorField = errorField;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
        public string ErrorField { get; }

        public bool Valid(TEntity entity)
        {
            return _specificationRule.IsSatisfiedBy(entity);
        }
    }
}