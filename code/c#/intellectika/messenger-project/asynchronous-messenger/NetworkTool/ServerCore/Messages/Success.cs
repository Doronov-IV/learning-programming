using Common;
using Common.Messages;

namespace ServerCore.Messages
{
	public class Success : ResponseMessage
	{
        public Success(string sender, string reciever, string date, string time, string? message = null) : base(RequestStatus.Success, sender, reciever, date, time, message)
        {
            //
        }

        public Success(string? message) : base(RequestStatus.Success, message)
        {
            //
        }
    }
}
