using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCore3AndTests.Infra.Configurations.Filters
{
    public class CorrelationIdValidateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrWhiteSpace(context.HttpContext.Request.Headers["X-Correlation-ID"]))
                context.Result = new BadRequestObjectResult(new { Code = "InvalidCorrelationID", Message = "Invalid correlation ID" });
        }
    }
}