namespace ServerCore.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class RequestMethodNameAttribute : Attribute
	{
		public readonly string MethodName;

		public RequestMethodNameAttribute(string methodName)
		{
			MethodName = methodName;
		}
	}
}
