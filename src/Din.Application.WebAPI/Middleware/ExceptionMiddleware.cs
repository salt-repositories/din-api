using System;
using System.Net;
using System.Threading.Tasks;
using Din.Domain.Exceptions.Abstractions;
using Din.Domain.Exceptions.Concrete;
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
                case HttpClientException clientException:
                    _logger.LogWarning(clientException, "A Client exception has been thrown");
                    return CreateResponse(context, clientException.Message, (int) clientException.StatusCode, clientException.ClientResponse);
                case DinException dinException:
                    _logger.LogWarning(dinException, "A Din exception has been thrown");
                    return CreateResponse(context, dinException.Message, (int) dinException.StatusCode, null);
                case ValidationException _:
                    _logger.LogWarning(exception, "A validation exception has been thrown");
                    return CreateResponse(context, exception.Message, (int) HttpStatusCode.BadRequest, null);
            }

            _logger.LogWarning(exception, "A unidentified exception has been thrown");
            return CreateResponse(context, exception.Message, (int) HttpStatusCode.InternalServerError, null);
        }

        private Task CreateResponse(HttpContext context, string message, int statusCode, object details)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = message, Details = details }));
        }
    }
}