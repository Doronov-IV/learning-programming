using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
	public class RequestBuilder
	{
		private readonly RequestProvider _provider;

		public RequestBuilder(Assembly assembly)
		{
			_provider = new RequestProvider(assembly);
		}

		internal RequestHandler Create(TcpClient client)
		{
			Connection connection = new Connection(client);

			var request = new RequestHandler(connection, _provider);

			return request;
		}
	}
}
