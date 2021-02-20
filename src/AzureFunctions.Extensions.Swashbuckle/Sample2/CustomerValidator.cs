using FluentValidation;
using Sample2;
namespace WidgetApi.FunctionHelpers
{ 
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Id).NotNull();
            RuleFor(order => order.Age).GreaterThan(0).LessThan(100).WithMessage("age-length");
            
        }

    } 
}