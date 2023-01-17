using System.Net;
using System.Net.Sockets;

namespace Common
{
	public class Connection
	{
		private readonly TcpClient _client;

		private readonly NetworkStream _stream;



		public Connection(IPAddress address, int port)
		{
			_client = new TcpClient(new IPEndPoint(address, port));
			_stream = _client.GetStream();
		}

        public Connection(string host, int port)
        {
            _client = new TcpClient(host, port);
            _stream = _client.GetStream();
        }

        public Connection(TcpClient client)
		{
			_client = client;
			_stream = _client.GetStream();
		}

		public async Task<byte[]?> RequestAsync(byte[] data)
		{
			await _stream.WriteAsync(data);

			return await ReceiveAsync();
		}

		public void Release()
		{
			_client.Close();
		}

		public async Task<byte[]?> ReceiveAsync()
		{
			byte[] data = new byte[256];

			int bytes;

			do
			{

				bytes = await _stream.ReadAsync(data, 0, data.Length);

			} while (_stream.DataAvailable);

			byte[] res = new byte[bytes];

			var list = data.ToList();

			list.RemoveRange(bytes, 256 - bytes);

			list.CopyTo(res);

			return res;
		}

		public async Task SendAsync(byte[] data)
		{
			await _stream.WriteAsync(data);
		}
	}
}