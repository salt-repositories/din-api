using System;
using System.Net;
using System.Threading.Tasks;
using Din.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Din.Application.WebAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case InvalidOperationException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case HttpClientException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case AuthenticationException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }

            var result = JsonConvert.SerializeObject(new {error = exception.Message});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}