using FluentValidation;

namespace EmployeeAPI.Validators
{
    public static class StringPropertyValidator
    {
        private static readonly char[] InvalidChars = {'{', '}', '"', '\''};

        public static IRuleBuilderOptions<T, string> StringValidator<T>(this IRuleBuilder<T, string> ruleBuilder,
            string parameterName)
        {
            return ruleBuilder
                .Must(text => !string.IsNullOrWhiteSpace(text) &&
                              text.IndexOfAny(InvalidChars) == -1)
                .WithMessage($"{parameterName} is required")
                .Length(1, 50)
                .WithMessage($"{parameterName} must be between 1 and 50 characters");
        }
    }
}