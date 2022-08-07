using FluentValidation;
using Library_.DTO;

namespace Library_.Validation
{
    public class RateBookComandValidator:AbstractValidator<RateDTO>
    {
        public RateBookComandValidator()
        {
            RuleFor(item=>item.Score).NotNull().WithMessage("You need to rate.").LessThanOrEqualTo(5).GreaterThanOrEqualTo(1).WithMessage("Scores must be from 1 to 5");
        }
    }
}
