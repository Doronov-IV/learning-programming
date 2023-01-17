using System.Net.Sockets;

namespace ServerCore
{
	public class Receiver
	{
		private readonly TcpListener _listener;

		private readonly RequestBuilder _builder;

		public Receiver(RequestBuilder builder, int port = 8888)
		{
			_listener = new TcpListener(System.Net.IPAddress.Any, port);
			_builder = builder;
		}

		public async Task StartAsync()
		{
			_listener.Start();
			Console.WriteLine("Server started");

			while (true) 
			{ 
				TcpClient tcpClient = await _listener.AcceptTcpClientAsync();

				var handler = _builder.Create(tcpClient);

				handler.Process();
			}
		}
	}
}