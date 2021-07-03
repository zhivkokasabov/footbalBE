using Core.contracts.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.middlewares
{
    public class FluentValidationMiddleware : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var fieldsWithErros = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0);

                var errorReponse = new ErrorResponse();

                foreach (var field in fieldsWithErros)
                {
                    foreach (var error in field.Value.Errors)
                    {
                        var errorModel = new ErrorModel
                        {
                            FieldName = field.Key,
                            Error = error.ErrorMessage
                        };

                        errorReponse.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorReponse);
                return;
            }

            await next();
        }
    }
}
