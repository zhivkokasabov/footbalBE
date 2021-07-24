using Core.Models;
using FluentValidation;

namespace Api.Validators
{
    public class NotificationValidator : AbstractValidator<Notification>
    {
        public NotificationValidator()
        {
            RuleFor(x => x.Message)
                .MaximumLength(256);

            RuleFor(x => x.RedirectUrl)
                .MaximumLength(128);
        }
    }
}
