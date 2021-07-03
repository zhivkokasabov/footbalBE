using Api.Controllers;
using Core.contracts.Response;
using Core.Enums;
using FluentValidation;

namespace Api.validators
{
    public class TournamentValidator: AbstractValidator<TournamentOutputDto>
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
                    x.TournamentTypeId == (int)TournamentType.Classic);

            RuleFor(x => x.TeamsCount)
                .Must(x => (x != 0) && ((x & (x - 1)) == 0)).When(x =>
                    x.TournamentTypeId == (int)TournamentType.Elimination)
                .WithMessage("Teams Count must be a power of 2");

            RuleFor(x => x.TeamsAdvancingAfterGroups)
                .NotNull().When(x =>
                    x.TournamentTypeId == (int)TournamentType.Classic)
                .LessThan(x => x.TeamsCount)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.GroupSize)
                .NotNull().When(x =>
                    x.TournamentTypeId == (int)TournamentType.Classic)
                .GreaterThanOrEqualTo(2);

            RuleFor(x => x.StartDate)
                .NotNull();

            RuleFor(x => x.Rules)
                .MaximumLength(2000);

            RuleFor(x => x.PlayingFields)
                .NotNull();

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
