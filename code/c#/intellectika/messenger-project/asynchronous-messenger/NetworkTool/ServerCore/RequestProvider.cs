using Common.Messages;
using ServerCore.Attributes;
using ServerCore.Messages;
using System.Reflection;

namespace ServerCore
{
	internal class RequestProvider
	{
		private readonly Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();



		public RequestProvider(Assembly assembly)
		{
			foreach (var type in assembly.GetTypes().Where(t => t.ContainsAttribute(typeof(RequestControllerAttribute))))
			{
				foreach (var method in type.GetMethods())
				{
					var attr = method.GetCustomAttribute<RequestMethodNameAttribute>();

					if (attr != null)
					{
						_methods.Add(attr.MethodName, method);
					}
				}
			}
		}

		public async Task<ResponseMessage> ProcessRequest(RequestMessage request)
		{
			if (_methods.TryGetValue(request.MethodName, out var method))
			{
				var instance = Activator.CreateInstance(method.DeclaringType);

				var rawResponce = method.Invoke(instance, new object[] { request });

				if (rawResponce is Task<ResponseMessage> response)
					return await response;

				else
					return new Error("Internal error");
			}

			return new Error("Unknown method");
		}
	}
}
