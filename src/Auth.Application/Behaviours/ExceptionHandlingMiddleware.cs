using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Auth.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Auth.Application.Behaviours
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppException appEx)
            {
                _logger.LogInformation(appEx, appEx.Message);
                await HandleExceptionAsync(context, appEx);
            }
            catch (Exception e)
            {
                string message = e.Message;
                _logger.LogError(e, message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext httpContext,
            Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception),
                innerMessage = exception.InnerException?.Message,
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                Exceptions.ValidationException => StatusCodes.Status422UnprocessableEntity,
                AppException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception)
        {
            var title = exception switch
            {
                AppException applicationException => "Test",
                _ => "Server Error"
            };

            return title ?? string.Empty;
        }

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            if (exception is Exceptions.ValidationException validationException)
                return validationException.Errors;

            return new Dictionary<string, string[]>();
        }
    }
}
