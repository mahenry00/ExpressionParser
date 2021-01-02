using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionParser
{
	public static class ExpressionParser
	{
		public static Delegate Parse(string input)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Parse(input);
		}

		public static Delegate ParseFor<TInput>(string input, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new[] { typeof(TInput) }).ParseFor<TInput>(input, parameterName);
		}


		public static Func<TOutput> Parse<TOutput>(string input)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Parse<TOutput>(input);
		}

		public static Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new [] { typeof(TInput), typeof(TOutput) }).ParseFor<TInput, TOutput>(input, parameterName);
		}

		public static LambdaExpression ParseExpression(string input)
		{
			var parser = new ExpressionParserImplementation();
			return parser.ParseExpression(input);
		}

		public static LambdaExpression ParseExpressionFor<TInput>(string input, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new[] { typeof(TInput) }).ParseExpressionFor<TInput>(input, parameterName);
		}

		public static LambdaExpression ParseExpressionFor(string input, Type inputType, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new[] { inputType }).ParseExpressionFor(input, inputType, parameterName);
		}

		public static Expression<Func<TOutput>> ParseExpression<TOutput>(string input)
		{
			var parser = new ExpressionParserImplementation();
			return parser.ParseExpression<TOutput>(input);
		}

		public static Expression<Func<TInput, TOutput>> ParseExpressionFor<TInput, TOutput>(string input, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new[] { typeof(TInput), typeof(TOutput) }).ParseExpressionFor<TInput, TOutput>(input, parameterName);
		}

		public static IExpressionParser Using(Type type, string alias = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(type, alias);
		}

		public static IExpressionParser Using(IEnumerable<Type> types)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(types);
		}

		public static IExpressionParser Using(IDictionary<Type, string> typeMap)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(typeMap);
		}
	}
}