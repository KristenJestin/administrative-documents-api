﻿using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace WebApi.Extensions
{
	public class SlugifyParameterTransformer : IOutboundParameterTransformer
	{
		public string TransformOutbound(object value)
			=> value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
	}
}
