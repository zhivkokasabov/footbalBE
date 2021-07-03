using Core.Contracts.Request.Teams;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class TeamValidator: AbstractValidator<TeamDto>
    {
        public TeamValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(4)
                .MaximumLength(64);
        }
    }
}
