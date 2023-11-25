using EGIDTask.Models.Criteria.Orders;
using FluentValidation;

namespace EGIDTask.Validation.FluentValidation
{
    public class GetStocksCriteriaValidator : AbstractValidator<GetStocksCriteria>
    {
        public GetStocksCriteriaValidator()
        {
            Include(new GetGenaricValidator());
        }
    }
    public class GetOrdersCriteriaValidator : AbstractValidator<GetOrdersCriteria>
    {
        public GetOrdersCriteriaValidator()
        {
            Include(new GetGenaricValidator());
        }
    }

}
