using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

using AutoMapper.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace SE4Autism.HighScoreTracker.Middleware
{
	public class SpaCatchMiddleware
	{

		private readonly RequestDelegate _next;

		public SpaCatchMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{
			if (!httpContext.Request.Path.StartsWithSegments(new PathString("/api")))
				return _next(httpContext);

			return Task.CompletedTask;
		}

	}

	public static class SpaCatchMiddlewareExtensions
	{

		public static IApplicationBuilder UseSpaCatchMiddleware(
			this IApplicationBuilder applicationBuilder)
			=> applicationBuilder.UseMiddleware<SpaCatchMiddleware>();
	}
}
