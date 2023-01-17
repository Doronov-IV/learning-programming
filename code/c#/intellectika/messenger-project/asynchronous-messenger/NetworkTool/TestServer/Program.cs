
using ServerCore;
using System.Reflection;

public static class Program
{
	public static async Task Main()
	{
		var assembly = Assembly.GetAssembly(typeof(Program));

		RequestBuilder builder = new RequestBuilder(assembly);

		Receiver server = new Receiver(builder);

		await server.StartAsync();
	}
}