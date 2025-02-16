﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
	public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError("Error message {exceptionmessage} on time {time}", exception.Message, DateTime.UtcNow);

			(string Message, string Title, int StatusCode) details = exception switch
			{
				BadRequestException => (
					exception.Message,
					exception.GetType().Name,
					context.Response.StatusCode = StatusCodes.Status400BadRequest
				),
				InternalServerErrorException => (
					exception.Message,
					exception.GetType().Name,
					context.Response.StatusCode = StatusCodes.Status500InternalServerError
				),
				ValidationException => (
					exception.Message,
					exception.GetType().Name,
					context.Response.StatusCode = StatusCodes.Status400BadRequest
				),
				NotFoundException => (
					exception.Message,
					exception.GetType().Name,
					context.Response.StatusCode = StatusCodes.Status404NotFound
				),
				_ =>(
					exception.Message,
					exception.GetType().Name,
					context.Response.StatusCode = StatusCodes.Status500InternalServerError
				)
			};
			var problemsDetails = new ProblemDetails
			{
				Status = details.StatusCode,
				Title = details.Title,
				Detail = details.Message,
				Instance = context.Request.Path
			};

			problemsDetails.Extensions.Add("traceId", context.TraceIdentifier);
			if(exception is ValidationException validationException)
			{
				problemsDetails.Extensions.Add("errors", validationException.Errors);
			}
			
		 await context.Response.WriteAsJsonAsync(problemsDetails, cancellationToken);
			
			return true;
		}

	}
}
