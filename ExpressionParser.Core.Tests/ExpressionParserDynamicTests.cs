using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ExpressionParser.Core.Tests
{
	[TestFixture]
	class ExpressionParserDynamicTests
	{

		[Test]
		public void Test()
		{
			string json = @"
{
	""Obj"": {
		""Prop1"": ""one""
		""Prop2"": ""two""
	}
}";

			JObject j = JObject.Parse(json);
			dynamic d = j;

			var exp = ExpressionParser.ParseExpressionFor<JObject, bool>("Obj.Prop1 == \"one\" && Obj.Prop2 == \"two\"");

			var val = exp.Compile()(j);
		}
	}
}
