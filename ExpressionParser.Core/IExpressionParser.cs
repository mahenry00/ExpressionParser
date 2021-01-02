using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionParser
{
	public interface IExpressionParser
	{
		Delegate Parse(string input);
		LambdaExpression ParseExpression(string input);

		Func<TOutput> Parse<TOutput>(string input);
		Expression<Func<TOutput>> ParseExpression<TOutput>(string input);
		LambdaExpression ParseExpressionFor(string input, Type inputType, string parameterName = null);

		Delegate ParseFor<TInput>(string input, string parameterName = null);
		LambdaExpression ParseExpressionFor<TInput>(string input, string parameterName = null);

		Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName = null);
		Expression<Func<TInput, TOutput>> ParseExpressionFor<TInput, TOutput>(string input, string parameterName = null);

		IExpressionParser Using(Type type, string alias = null);
		IExpressionParser Using(IEnumerable<Type> types);
		IExpressionParser Using(IDictionary<Type, string> typeMaps);
	}
}