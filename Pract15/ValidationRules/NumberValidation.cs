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

            char decimalSeparator = culture.NumberFormat.NumberDecimalSeparator[0];

            bool isValidNumber = true;
            int decimalSeparatorCount = 0;

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    if (c == decimalSeparator)
                    {
                        decimalSeparatorCount++;
                        if (decimalSeparatorCount > 1)
                        {
                            isValidNumber = false;
                            break;
                        }
                    }
                    else
                    {
                        isValidNumber = false;
                        break;
                    }
                }
            }

            if (!isValidNumber)
            {
                return new ValidationResult(false, $"Поле может состоять только из цифр и разделителя '{decimalSeparator}'");
            }

            if (input.StartsWith(decimalSeparator.ToString()) || input.EndsWith(decimalSeparator.ToString()))
            {
                return new ValidationResult(false, $"Число не может начинаться или заканчиваться разделителем '{decimalSeparator}'");
            }

            if (decimal.TryParse(input, NumberStyles.Number, culture, out decimal number))
            {
                if (number <= 0)
                {
                    return new ValidationResult(false, "Значение должно быть больше 0");
                }
            }
            else
            {
                return new ValidationResult(false, $"Некорректный формат числа. Используйте разделитель '{decimalSeparator}'");
            }

            return ValidationResult.ValidResult;
        }
    }
}
