using EGIDTask.Models.Criteria;
using FluentValidation;

namespace EGIDTask.Validation.FluentValidation
{
    public class GetGenaricValidator : AbstractValidator<BaseCriteria>
    {
        public GetGenaricValidator()
        {
            RuleFor(model => model).Must(m => m.PagingEnabled).
                    WithMessage("Sorry You Must Enable Pagination");

            RuleFor(model => model).Must(m => m.PageSize <= 5).
                    WithMessage("Sorry Page Size Must Less Than Or Equal5");
        }
    }
}
