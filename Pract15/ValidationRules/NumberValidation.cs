using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.ValidationRules
{
    public class NumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Ввод в поле обязателен");
            }

            CultureInfo culture = cultureInfo ?? CultureInfo.CurrentCulture;

            bool isDigital = true;
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    isDigital = false;
                    break;
                }
            }

            if (!isDigital)
            {

                return new ValidationResult(false, "Поле может состоять только из цифр");
            }

            if (input.Length < 0)
            {
                return new ValidationResult(false, "Значение должно быть больше 0");
            }

            return ValidationResult.ValidResult;
        }

    }
}
