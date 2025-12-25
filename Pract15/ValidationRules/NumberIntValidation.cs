using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.ValidationRules
{
    public class NumberIntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Ввод в поле обязателен");
            }

            if (!input.All(char.IsDigit))
            {
                return new ValidationResult(false, "Допускаются только цифры");
            }

            if (int.TryParse(input, out int intValue))
            {
                if (intValue <= 0)
                {
                    return new ValidationResult(false, "Значение должно быть больше 0");
                }
                return ValidationResult.ValidResult;
            }
            else if (long.TryParse(input, out long longValue))
            {
                if (longValue <= 0)
                {
                    return new ValidationResult(false, "Значение должно быть больше 0");
                }
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Некорректное целое число");
        }
    }
}
