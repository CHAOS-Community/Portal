namespace Chaos.Portal.Core.Bindings.Standard
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Reflection;
	using CHAOS.Extensions;
	using Exceptions;

	public class DateTimeParameterBinding : IParameterBinding
	{
		private const string DatePatternOne = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";
		private const string DatePatternTwo = "dd'-'MM'-'yyyy HH':'mm':'ss";

		public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
		{
			if (parameters.ContainsKey(parameterInfo.Name) && !string.IsNullOrEmpty(parameters[parameterInfo.Name]))
			{
				DateTime dt;

				if (DateTime.TryParseExact(parameters[parameterInfo.Name], DatePatternOne, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dt))
					return dt;

				return DateTime.ParseExact(parameters[parameterInfo.Name], DatePatternTwo, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
			}

			if (parameterInfo.ParameterType.IsNullable())
				return null;

			throw new ParameterBindingMissingException("The parameter is missing, and the type isnt nullable");
		}
	}
}