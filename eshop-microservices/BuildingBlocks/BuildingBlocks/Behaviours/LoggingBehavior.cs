using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviours
{
	public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
		where TRequest : notnull, IRequest<TResponse>
		where TResponse : notnull
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			//before request is executed
			logger.LogInformation("[Start] Handle={request} - Response = {Response} - RequestData={request}", typeof(TRequest).Name, typeof(TResponse).Name, request);
			Stopwatch timer = new Stopwatch();
			timer.Start();

			var response = await next();
			//after request is executed
			timer.Stop();

			var timelapse = timer.Elapsed;
			if(timelapse.Seconds > 3){
				logger.LogWarning("[PERFORMANCE] the request {Request} took {timeTaken} seconds to be completed", typeof(TRequest).Name, timelapse.Seconds);
			}
			logger.LogInformation("[END] the request {request} finished with response {response}", typeof(TRequest).Name, response);
			return response;
		}
	}
}
