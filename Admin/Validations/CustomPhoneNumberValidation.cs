

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Admin.Validations
{
    public class CustomPhoneNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string pattern = @"^\d{3}-\d{2}-\d{3}-\d{2}-\d{2}$";
            return Regex.IsMatch((string)value, pattern);
        }

        



    }
}
