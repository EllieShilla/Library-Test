using FluentValidation;
using Library_.DTO;

namespace Library_.Validation
{
    public class CreateBookComandValidator:AbstractValidator<BookForSaveDTO>
    {
        public CreateBookComandValidator()
        {
            RuleFor(item => item.Title).NotEmpty().WithMessage("Please specify a Title").MinimumLength(3).WithMessage("The length of the text must be 3 characters or more"); 
            RuleFor(item => item.Author).NotEmpty().WithMessage("Please specify a Author").MinimumLength(3).WithMessage("The length of the text must be 3 characters or more"); 
            RuleFor(item => item.Genre).NotEmpty().WithMessage("Please specify a Genre").MinimumLength(3).WithMessage("The length of the text must be 3 characters or more"); 
            RuleFor(item => item.Content).NotEmpty().WithMessage("Please specify a Content").MinimumLength(3).WithMessage("The length of the text must be 3 characters or more");
            RuleFor(item => item.Cover).NotEmpty().WithMessage("Please specify a Cover");
        }
    }
}
