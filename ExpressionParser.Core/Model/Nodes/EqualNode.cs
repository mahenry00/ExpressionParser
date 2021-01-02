using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class EqualNode : BinaryNode
	{
		internal EqualNode() : base(7) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			(var left, var right) = base.BuildLeftAndRight(callerExpression, true);
			return Expression.Equal(left, right);
		}
	}
}