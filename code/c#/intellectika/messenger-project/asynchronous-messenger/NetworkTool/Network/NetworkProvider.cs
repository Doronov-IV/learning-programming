using Common;
using Common.Messages;
using System.Net;

namespace Network
{
	public class NetworkProvider
	{
		private readonly IPAddress _address;

		private string _host;

		private readonly int _port;



		public NetworkProvider(IPAddress address, int port = 8888)
		{
			_address = address;
			_port = port;
		}

		public NetworkProvider(string host = "localhost", int port = 8888)
		{
			_host = host;
			_port = port;
		}

		public async Task<ResponseMessage> GetAsync(RequestMessage requestData)
		{
			Connection? connection = new Connection(_host, _port);

			var data = SerializationHelper.Serialize(requestData);

			var response = await connection.RequestAsync(data.ToArray());

			connection.Release();

			if (response != null)
			{
				return SerializationHelper.Deserialize<ResponseMessage>(response);
			}

			throw new Exception("Response is null");
		}
	}
}