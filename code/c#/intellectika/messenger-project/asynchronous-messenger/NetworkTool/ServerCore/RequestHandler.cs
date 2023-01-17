using Common;
using Common.Messages;

namespace ServerCore
{
	internal class RequestHandler
	{
		private readonly Connection _connection;

		private readonly RequestProvider _provider;



		public RequestHandler(Connection connection, RequestProvider provider)
		{
			_connection = connection;
			_provider = provider;
		}

		public Task Process() => Task.Run(ProcessAsync);

		public async Task ProcessAsync()
		{
			var data = await _connection.ReceiveAsync();

			if (data != null)
			{
				var request = SerializationHelper.Deserialize<RequestMessage>(data);

				var response = (await _provider.ProcessRequest(request)).GetResponseMessage();

				var outData = SerializationHelper.Serialize(response);

				await _connection.SendAsync(outData.ToArray());
			}

			_connection.Release();
		}
	}
}
