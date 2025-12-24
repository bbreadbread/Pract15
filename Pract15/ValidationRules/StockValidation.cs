using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.ValidationRules
{
    class StockValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

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

            if (int.TryParse(input, out int i))
                if (i < 10)
                {
                    return new ValidationResult(false, "");
                }

            return ValidationResult.ValidResult;
        }
    }
}
