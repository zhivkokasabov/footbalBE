using Core.Contracts.Request;
using FluentValidation;
using System.Net.Mail;

namespace Api.Validators
{
    public class UserValidator: AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .Custom((email, context) =>
                {
                    try
                    {
                        var mailAddress = new MailAddress(email);

                        if (mailAddress.Address != email)
                        {
                            context.AddFailure("Not a valid email format");
                        }
                    } catch
                    {
                        context.AddFailure("Not a valid email format");
                    }
                });

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
