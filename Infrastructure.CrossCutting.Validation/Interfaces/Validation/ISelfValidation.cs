namespace Infrastructure.CrossCutting.Validation.Interfaces.Validation
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
        bool IsNotValid { get; }
    }
}