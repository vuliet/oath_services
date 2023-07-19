using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Application.Extensions;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();

            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            _logger.LogInformation($"---------- [START] {requestName} ----------");

            //_logger.LogInformation("Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            var response = await next();

            _logger.LogInformation($"---------- [END] {requestName} ----------");

            // _logger.LogInformation("Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }

}