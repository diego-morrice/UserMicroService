namespace Infrastructure.CrossCutting.Validation
{
    public class ValidationError
    {
        public string Message { get; private set; }
        public string Field { get; private set; }

        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}