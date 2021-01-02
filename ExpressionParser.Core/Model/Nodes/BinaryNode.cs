using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal abstract class BinaryNode : OperationNode
	{
		protected BinaryNode(int precedence) : base(precedence) { }

		internal Node Left { get; set; }
		internal Node Right { get; set; }

		internal override bool IsClosed => (Left?.IsClosed ?? false) && Right.IsClosed;

		internal override bool TryAddNode(Node node)
		{
			if (Right != null) return Right.TryAddNode(node);
			Right = node;
			return true;
		}

		protected (Expression Left, Expression Right) BuildLeftAndRight(Expression callerExpression, bool withConversion = false)
		{
			var left = Left.BuildExpression(callerExpression);
			var right = Right.BuildExpression(callerExpression);

			if (withConversion && left.Type != right.Type)
			{
				if (left.NodeType == ExpressionType.Constant)
					right = Expression.Convert(right, left.Type);
				else
					left = Expression.Convert(left, right.Type);
			}

			return (left, right);
		}
	}
}