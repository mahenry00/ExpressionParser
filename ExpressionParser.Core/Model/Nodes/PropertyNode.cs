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
							if (getMethod != null)
								return Expression.Call(callerExpression, getMethod, Expression.Constant(Name));
							else
							{
								// For System.Text.JsonElement
								member = callerExpression.Type.GetProperty("GetProperty", new[] { typeof(string) });
								if (member != null)
									return Expression.Property(callerExpression, member, Expression.Constant(Name));
							}
						}

						throw new MissingMemberException();
					}
			}
		}
	}
}