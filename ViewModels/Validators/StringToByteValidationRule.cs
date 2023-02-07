using System.Windows.Controls;

namespace ViewModels.Validators
{
    ///<inheritdoc/>
    public class StringToByteValidationRule : ValidationRule
    {
        ///<inheritdoc/>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            byte i;
            if (byte.TryParse(value.ToString(), out i))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid integer value.");
        }
    }
}
