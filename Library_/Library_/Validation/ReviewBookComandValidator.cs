using FluentValidation;
using Library_.DTO;

namespace Library_.Validation
{
    public class ReviewBookComandValidator : AbstractValidator<ReviewDTO>
    {
        public ReviewBookComandValidator()
        {
            RuleFor(item => item.Message).NotEmpty().WithMessage("Please specify a message").MinimumLength(3).WithMessage("The length of the text must be 3 characters or more");
            RuleFor(item => item.Reviwer).NotEmpty().WithMessage("Please specify a reviewer").MinimumLength(3).WithMessage("The length of the text must be 3 characters or more");
        }
    }
}
