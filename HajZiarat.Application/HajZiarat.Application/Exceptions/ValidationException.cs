using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException : ApplicationException
    {

        public List<string> Errors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult): this(GetError(validationResult))
        {
        }

        public ValidationException(List<ValidationFailure> failures) : this(GetError(failures))
        {
        }


        public ValidationException(List<string> errors) : this(string.Join(" | ", errors))
        {
            Errors = errors;
        }

        public ValidationException(string message): base(message)
        {
            Errors = new List<string> { message };
        }


        /// <summary>
        /// گرفتن متن خطا از ولیدیتور
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        private static List<string> GetError(ValidationResult validationResult)
        {
            return validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        }

        private static List<string> GetError(List<ValidationFailure> failures)
        {
            return failures.Select(x => x.ErrorMessage).ToList();
        }
    }
}
