using Common;
using Common.Messages;

namespace ServerCore.Messages
{
	public class Error : ResponseMessage
	{
		public Error(string sender, string reciever, string date, string time, string? message = null) : base(RequestStatus.Error, sender, reciever, date, time, message)
		{
			//
		}

		public Error(string? message) : base(RequestStatus.Error, message)
		{
			//
		}
	}
}
