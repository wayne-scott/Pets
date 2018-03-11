using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pets.WebSite.Pages
{
    public class ErrorModel : PageModel
    {
        public bool RouteWhereExceptionOccurredExists => !string.IsNullOrEmpty(RouteWhereExceptionOccurred);
        [DisplayName("Route where exception occurred.")]
        public string RouteWhereExceptionOccurred { get; set; }

        public bool ExceptionMessageExists => !string.IsNullOrEmpty(ExceptionMessage);
        [DisplayName("Exception message.")]
        public string ExceptionMessage { get; set; }

        public void OnGet()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                RouteWhereExceptionOccurred = exceptionFeature.Path;
                ExceptionMessage = exceptionFeature.Error.Message;
            }
        }
    }
}
