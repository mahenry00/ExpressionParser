using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class ArrayIndexNode : BinaryNode
	{
		internal ArrayIndexNode() : base(0) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var left = Left.BuildExpression(callerExpression);
			var right = Right.BuildExpression(callerExpression);
			return Expression.MakeIndex(left, left.Type.GetProperty("Item", new[] { right.Type }), new [] { right });
		}
	}
}