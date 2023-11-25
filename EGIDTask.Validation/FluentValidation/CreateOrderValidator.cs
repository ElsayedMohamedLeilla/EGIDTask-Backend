using EGIDTask.Models.Dtos.Orders;
using FluentValidation;

namespace EGIDTask.Validation.FluentValidation
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderModel>
    {
        public CreateOrderValidator()
        {
            RuleFor(model => model.PersonName).NotNull().
                    WithMessage("Sorry You Must Enter Person Name");

            RuleFor(model => model.StockId).GreaterThan(0).
                    WithMessage("Sorry You Must Choose Stock");

            RuleFor(model => model.Quantity).GreaterThan(0).
                   WithMessage("Sorry You Must Enter Quantity");

            RuleFor(model => model.Price).GreaterThan(0).
                    WithMessage("Sorry You Must Enter Price");
        }
    }
}
