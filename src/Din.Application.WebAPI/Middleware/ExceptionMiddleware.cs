using System;
using System.Net;
using System.Threading.Tasks;
using Din.Application.WebAPI.Serilization;
using Din.Domain.Exceptions.Abstractions;
using Din.Domain.Exceptions.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Din.Application.WebAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
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
                    return CreateResponse(context, (int) clientException.StatusCode, new
                    {
                        clientException.Message,
                        Errors = clientException.ClientResponse
                    });
                case DinException dinException:
                    _logger.LogWarning(dinException, "A Din exception has been thrown");
                    return CreateResponse(context, (int) dinException.StatusCode, new
                    {
                        dinException.Message,
                        Errors = dinException.Details
                    });
                case ValidationException validationException:
                    return CreateResponse(context, (int) HttpStatusCode.BadRequest, new
                    {
                        validationException.Message,
                        validationException.Errors,
                    });
            }

            _logger.LogError(exception, "A unidentified exception has been thrown");

            return CreateResponse(context, (int) HttpStatusCode.InternalServerError, new
            {
                Message = _environment.IsDevelopment()
                    ? exception.Message
                    : "Something went wrong"
            });
        }

        private Task CreateResponse(HttpContext context, int statusCode, object response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject
            (
                response,
                SerializationSettings.GetSerializerSettings()
            ));
        }
    }
}