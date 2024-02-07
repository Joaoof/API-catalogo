using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters
{
    public class ApiExecptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExecptionFilter> _logger;

        public ApiExecptionFilter(ILogger<ApiExecptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "There was a problem with the request: Status Code 500");

            context.Result = new ObjectResult("There was a problem with the request: Status Code 500")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
