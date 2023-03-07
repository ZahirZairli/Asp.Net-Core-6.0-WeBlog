using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kateqoriya adını daxil edin");
            RuleFor(x => x.CategoryName).MaximumLength(20).WithMessage("Maksimum 20 simvol daxil edin");
            RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Minimum 3 simvol daxil edin");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Kateqoriya təsvirini daxil edin");
        }
    }
}
