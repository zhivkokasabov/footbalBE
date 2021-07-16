using Core.contracts.Request;
using Core.Enums;
using FluentValidation;

namespace Api.validators
{
    public class TournamentValidator: AbstractValidator<TournamentDto>
    {
        public TournamentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(4, 50);

            RuleFor(x => x.Avenue)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(2000);

            RuleFor(x => x.TeamsCount)
                .NotNull().When(x =>
                    x.TournamentTypeId == (int)TournamentTypes.Classic);

            RuleFor(x => x.TeamsCount)
                .Must(x => (x != 0) && ((x & (x - 1)) == 0)).When(x =>
                    x.TournamentTypeId == (int)TournamentTypes.Elimination)
                .WithMessage("Teams Count must be a power of 2");

            RuleFor(x => x.TeamsAdvancingAfterGroups)
                .LessThan(x => x.TeamsCount)
                .LessThan(x => x.GroupSize)
                .GreaterThanOrEqualTo(1)
                .NotNull().When(x =>
                    x.TournamentTypeId == (int)TournamentTypes.Classic);

            RuleFor(x => x.GroupSize)
                .NotNull().When(x =>
                    x.TournamentTypeId == (int)TournamentTypes.Classic)
                .GreaterThanOrEqualTo(2)
                .LessThanOrEqualTo(12);

            RuleFor(x => x.StartDate)
                .NotNull();

            RuleFor(x => x.Rules)
                .MaximumLength(2000);

            RuleFor(x => x.PlayingFields)
                .NotNull()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.MatchLength)
                .NotNull();

            RuleFor(x => x.HalfTimeLength)
                .NotNull();

            RuleFor(x => x.PlayingDaysId)
                .NotNull()
                .InclusiveBetween(1, 3);

            RuleFor(x => x.TournamentAccessId)
                .NotNull()
                .InclusiveBetween(1, 3);

            RuleFor(x => x.TournamentTypeId)
                .NotNull()
                .InclusiveBetween(1, 3);
        }
    }
}
