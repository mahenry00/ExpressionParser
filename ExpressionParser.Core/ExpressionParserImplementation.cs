using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionParser.Engine;

namespace ExpressionParser
{
	internal class ExpressionParserImplementation : IExpressionParser
	{
		private readonly Reader reader = new Reader();

		public Delegate Parse(string input)
		{
			return ParseExpression(input).Compile();
		}

		public Func<TOutput> Parse<TOutput>(string input)
		{
			return (Func<TOutput>)Parse(input);
		}

		public LambdaExpression ParseExpression(string input)
		{
			var tokens = reader.ReadFrom(input);
			var expression = Builder.BuildExpression(tokens);
			return expression;
		}

		public Expression<Func<TOutput>> ParseExpression<TOutput>(string input)
		{
			return (Expression<Func<TOutput>>)ParseExpression(input);
		}

		public LambdaExpression ParseExpressionFor(string input, Type inputType, string parameterName = null)
		{
			var tokens = reader.ReadFrom(input);
			var expression = Builder.BuildExpressionFor(inputType, tokens, parameterName);
			return expression;
		}

		public LambdaExpression ParseExpressionFor<TInput>(string input, string parameterName = null)
		{
			var tokens = reader.ReadFrom(input);
			var expression = Builder.BuildExpressionFor<TInput>(tokens, parameterName);
			return expression;
		}

		public Expression<Func<TInput, TOutput>> ParseExpressionFor<TInput, TOutput>(string input, string parameterName = null)
		{
			return (Expression<Func<TInput, TOutput>>)ParseExpressionFor<TInput>(input, parameterName);
		}

		public Delegate ParseFor<TInput>(string input, string parameterName)
		{
			return ParseExpressionFor<TInput>(input, parameterName).Compile();
		}

		public Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName)
		{
			return (Func<TInput, TOutput>)ParseFor<TInput>(input, parameterName);
		}

		public IExpressionParser Using(Type type, string alias)
		{
			Reader.AddTypeMap(alias, type);
			return this;
		}

		public IExpressionParser Using(IEnumerable<Type> types)
		{
			if (types == null) throw new ArgumentNullException(nameof(types));
			foreach (var type in types)
				Reader.AddTypeMap(type.Name, type);
			return this;
		}

		public IExpressionParser Using(IDictionary<Type, string> typeMaps)
		{
			if (typeMaps == null) throw new ArgumentNullException(nameof(typeMaps));
			foreach (var typeMap in typeMaps)
				Reader.AddTypeMap(typeMap.Value, typeMap.Key);
			return this;
		}
	}
}