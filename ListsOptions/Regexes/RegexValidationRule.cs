using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ListsOptionsUI.Regexes
{
    public class RegexValidationRule : ValidationRule
    {
        public string Pattern { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputValue = value as string;

            if (string.IsNullOrEmpty(inputValue) || !Regex.IsMatch(inputValue, Pattern))
            {
                return new ValidationResult(false, "Невалидна стойност");
            }

            return ValidationResult.ValidResult;
        }
    }
}
