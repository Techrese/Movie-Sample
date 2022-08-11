using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GenreApi.Models
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private StatusCodeResult _status = new StatusCodeResult(StatusCodes.Status400BadRequest);
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = _status;
                context.ExceptionHandled = true;
            }
        }        
    }
}
