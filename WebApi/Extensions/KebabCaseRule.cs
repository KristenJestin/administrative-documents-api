using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Primitives;

namespace WebApi.Extensions
{
	public class KebabCaseRule : IRule
	{
		public void ApplyRule(RewriteContext context)
		{
			QueryCollection query = new QueryCollection(
				context.HttpContext.Request.Query.Keys
					.Select(key =>
					{
						string[] nameParts = key.Split('-');
						for (var i = 0; i < nameParts.Length; i++)
							nameParts[i] = char.ToUpper(nameParts[i][0]) + nameParts[i].Substring(1);

						return new KeyValuePair<string, StringValues>(string.Join(string.Empty, nameParts), context.HttpContext.Request.Query[key]);
					})
					.ToDictionary(d => d.Key, d => d.Value)
			);
			context.HttpContext.Request.Query = query;
		}
	}
}
