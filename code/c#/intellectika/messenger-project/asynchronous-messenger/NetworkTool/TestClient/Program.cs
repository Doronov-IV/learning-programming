using Common.Messages;
using Network;
using System.Net;

class Program
{
	public static async Task Main()
	{
		var provider = new NetworkProvider();

		for (int i = 0; i < 1; i++)
		{
			Task.Run(async () =>
			{
				try
				{
					var responce = await provider.GetAsync(new RequestMessage() { MethodName = "Greeting" });

					Console.WriteLine(responce.ToString());
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			});
		}

		Console.Read();
	}
}