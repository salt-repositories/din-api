using System;
using System.Net;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Din.Application.WebAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case DinException _:
                    _logger.LogWarning(exception, "A Din exception has been thrown");
                    return CreateResponse(context, exception.Message, (int) ((DinException) exception).StatusCode);
                case ValidationException _:
                    _logger.LogWarning(exception, "A validation exception has been thrown");
                    return CreateResponse(context, exception.Message, (int) HttpStatusCode.BadRequest);
            }

            _logger.LogWarning(exception, "A unidentified exception has been thrown");
            return CreateResponse(context, exception.Message, (int) HttpStatusCode.InternalServerError);
        }

        private Task CreateResponse(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new {Message = message}));
        }
    }
}