using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Pylon.Application.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class PaginatedQueryAttributeAttribute : ActionFilterAttribute
	{
		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (context.Result is ObjectResult objectResult && objectResult.Value is IQueryable queryable)
			{
				var request = context.HttpContext.Request;
				var queryParams = request.Query;

				// Extract query string parameters
				int skip = int.TryParse(queryParams["Skip"], out var s) ? s : 0;
				int pageSize = int.TryParse(queryParams["PageSize"], out var ps) ? ps : 10;
				string orderBy = queryParams["OrderBy"].ToString(); // Example: "Name desc"

				// Apply sorting (if provided)
				if (!string.IsNullOrEmpty(orderBy))
				{
					try
					{
						// Apply dynamic ordering using System.Linq.Dynamic.Core
						queryable = queryable.OrderBy(orderBy);
					}
					catch
					{
						// If ordering fails (wrong column name), ignore it
					}
				}

				// Apply pagination
				queryable = queryable.Skip(skip).Take(pageSize);

				// Convert to List asynchronously
				var list = await Task.Run(() => queryable.Cast<object>().ToList());

				context.Result = new ObjectResult(list);
			}

			await next();
		}
	}
}
