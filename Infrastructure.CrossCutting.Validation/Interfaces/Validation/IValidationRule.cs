namespace Infrastructure.CrossCutting.Validation.Interfaces.Validation
{
    public interface IValidationRule<in TEntity>
    {
        string ErrorMessage { get; }
        string ErrorField { get; }
        bool Valid(TEntity entity);
    }
}