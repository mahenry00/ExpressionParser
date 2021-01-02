using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class NotEqualNode : BinaryNode
	{
		internal NotEqualNode() : base(7) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			(var left, var right) = base.BuildLeftAndRight(callerExpression, true);
			return Expression.NotEqual(left, right);
		}
	}
}