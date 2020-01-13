using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class Criteria : Value<Criteria>
	{
		public CriteriaExpressionTree Expression { get; }
		public CriteriaEvaluationType EvaluationType { get; }
		public List<CriteriaCondition> Conditions { get; }

		public Criteria(List<CriteriaCondition> conditions, string expression)
		{

		}
	}

	public enum CriteriaEvaluationType
	{
		And,
		Or,
		Custom
	}

	public class CriteriaCondition
	{

	}

	// https://www.geeksforgeeks.org/evaluation-of-expression-tree/
	public class CriteriaExpressionTree : Value<CriteriaExpressionTree>
	{
		public Node Root { get; }



		public class Node
		{

		}
		public class OperatorNode : Node
		{

		}
		public class ConditionNode : Node
		{

		}

		public enum Operator
		{
			And,
			Or
		}
	}
}
