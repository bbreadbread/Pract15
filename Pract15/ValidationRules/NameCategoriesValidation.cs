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
    class NameCategoriesValidation : ValidationRule
    {
        public CategoriesService service { get; set; } = new();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            foreach (Category cat in CategoriesService.Items)
            {
                if (cat.Name == input)
                    return new ValidationResult(false, "Категория с таким наименованием уже существует");
            }
            return ValidationResult.ValidResult;
        }
    }
}
