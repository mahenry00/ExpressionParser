using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class PropertyNode : IdentifierNode
	{
		internal PropertyNode(string name) : base(name, 99) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			switch (callerExpression)
			{
				case null: throw new InvalidOperationException($"Unknow identifier '{Name}'.");
				case ParameterExpression parameterExpression when parameterExpression.Name == Name: return callerExpression;
				default:
					{
						var member = callerExpression.Type.GetProperty(Name);
						if (member != null)
						{
							return Expression.PropertyOrField(callerExpression, Name);
						}
						else
						{
							var getMethod = callerExpression.Type.GetProperty("Item", new[] { typeof(string) })?.GetGetMethod();

							// For System.Text.JsonElement
							if (getMethod == null)
								getMethod = callerExpression.Type.GetMethod("GetProperty", new[] { typeof(string) });

							if (getMethod != null)
								return Expression.Call(callerExpression, getMethod, Expression.Constant(Name));
						}

						throw new MissingMemberException();
					}
			}
		}
	}
}