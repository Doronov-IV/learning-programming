using Common.Messages;
using ServerCore.Attributes;
using ServerCore.Messages;

namespace TestServer
{
	[RequestController]
	public class MainController
	{
		private static int _count;

		[RequestMethodName("Greeting")]
		public async Task<ResponseMessage> GreetingAsync(RequestMessage request)
		{
			Random random = new Random();

			var index = ++_count;

			var sleep = random.Next(500) * 100;

			await Task.Delay(sleep);

			return new Success($"Hi, dude. Your number is {index}");
		}
	}
}
