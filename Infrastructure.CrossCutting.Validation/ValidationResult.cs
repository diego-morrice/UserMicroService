using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.CrossCutting.Validation
{
    public class ValidationResult
    {
        private readonly List<ValidationError> _errors;
        // ReSharper disable once UnusedMember.Local
        private string Message { get; set; }
        public bool IsValid { get { return !_errors.Any(); } }
        public bool IsNotValid { get { return !IsValid; } }
        public IEnumerable<ValidationError> Errors { get { return _errors; } }

        public ValidationResult()
        {
            _errors = new List<ValidationError>();
        }       

        public ValidationResult Add(string errorField, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage)) return this;

            _errors.Add(new ValidationError(errorField, errorMessage));

            return this;
        }

        public ValidationResult Add(ValidationError error)
        {
            if (string.IsNullOrWhiteSpace(error?.Message)) return this;

            _errors.Add(error);

            return this;
        }

        public ValidationResult Add(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return this;

            foreach (var vr in validationResults.Where(vr => vr != null))
            {
                _errors.AddRange(vr.Errors.Where(e => !string.IsNullOrWhiteSpace(e.Message)));
            }

            return this;
        }

        public ValidationResult Add(IEnumerable<ValidationResult> validationResults)
        {
            return validationResults == null ? this : Add(validationResults.ToArray());
        }

        public ValidationResult Remove(ValidationError error)
        {
            if (_errors.Contains(error))
                _errors.Remove(error);
            return this;
        }
    }
}
