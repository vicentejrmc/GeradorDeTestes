using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GeradorDeTestes.WebApp.ActionFilters;

public class ValidarModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not Controller controller)
            return;

        var modelState = context.ModelState;
        var viewModel = context.ActionArguments.Values.FirstOrDefault(x => x?.GetType().Name.EndsWith("ViewModel") == true);

        if (!modelState.IsValid && viewModel is not null)
            context.HttpContext.Items["ModelStateInvalid"] = true;
    }
}
