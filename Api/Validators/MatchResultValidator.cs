using Core.Contracts.Request.Tournaments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class MatchResultValidator: AbstractValidator<MatchResultDto>
    {
        public MatchResultValidator()
        {
            RuleFor(x => x.AwayTeamScore)
                .GreaterThanOrEqualTo(0)
                .LessThan(99)
                .WithMessage(x => "Team score should be between 0 and 99");

            RuleFor(x => x.HomeTeamScore)
                .GreaterThanOrEqualTo(0)
                .LessThan(99)
                .WithMessage(x => "Team score should be between 0 and 99");
        }
    }
}
