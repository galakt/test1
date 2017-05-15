using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Serilog;

namespace ConsoleAppK.Logging
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        private readonly ILogger _logger;

        public ApiExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            _logger.Error(context.Exception, $"{context.Request.ToString()}");
            context.Result = new FixErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "Oops! Sorry! Something went wrong."
            };

            base.Handle(context);
        }

        private class FixErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(Content),
                        RequestMessage = Request
                    };
                return Task.FromResult(response);
            }
        }
    }
}
