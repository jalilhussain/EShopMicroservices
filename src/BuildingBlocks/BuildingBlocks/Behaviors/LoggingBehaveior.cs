using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehaveior<TRequest, TResponse>
        (ILogger<LoggingBehaveior<TRequest, TResponse>> logger)
            : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop(); // Recommended: Stop the timer after 'next()' completes
            var timeTaken = timer.Elapsed;

            // 3. The Performance Condition
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {Time} seconds to complete.",
                    typeof(TRequest).Name, timeTaken.Seconds);
            }

            // 4. Log the End of the request
            logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            // FIX: You MUST return the response here!
            return response;
        }
    }
}