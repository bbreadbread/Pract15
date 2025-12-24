using Pract15.Models;
using Pract15.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.ValidationRules
{
    class NameBrandsValidation : ValidationRule
    {
        public BrandsService service { get; set; } = new();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            foreach (Brand cat in BrandsService.Items)
            {
                if (cat.Name == input)
                    return new ValidationResult(false, "Категория с таким наименованием уже существует");
            }
            return ValidationResult.ValidResult;
        }
    }
}
