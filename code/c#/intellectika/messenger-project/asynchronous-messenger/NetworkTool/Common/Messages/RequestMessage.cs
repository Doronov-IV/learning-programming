using ProtoBuf;

namespace Common.Messages
{
	[ProtoContract]
	public class RequestMessage
	{

        /// <inheritdoc cref="MethodName"/>
        protected string _methodName;


        /// <inheritdoc cref="Status"/>
        protected RequestStatus _status;


        /// <inheritdoc cref="Sender"/>
        protected string _sender;


        /// <inheritdoc cref="Reciever"/>
        protected string _reciever;


        /// <inheritdoc cref="Date"/>
        protected string _date;


        /// <inheritdoc cref="Time"/>
        protected string _time;


        /// <inheritdoc cref="Message"/>
        protected string? _message;




        /// <summary>
        /// The name of the method to proceed the request.
        /// <br />
        /// Имя метода, чтобы обработать запрос.
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }



        [ProtoMember(2)]
        /// <summary>
        /// The staus of the request.
        /// <br />
        /// Статус запроса.
        /// </summary>
        public RequestStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }


        [ProtoMember(3)]
        /// <summary>
        /// Sender nickname.
        /// <br />
        /// Никнейм отправителя.
        /// </summary>
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }


        [ProtoMember(4)]
        /// <summary>
        /// Reciever nickname.
        /// <br />
        /// Никнейм получателя.
        /// </summary>
        public string Reciever
        {
            get { return _reciever; }
            set { _reciever = value; }
        }


        [ProtoMember(5)]
        /// <summary>
        /// The message sending date.
        /// <br />
        /// Дата отправки сообщения.
        /// </summary>
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }


        [ProtoMember(6)]
        /// <summary>
        /// The message sending time.
        /// <br />
        /// Время отправки сообщения.
        /// </summary>
        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }


        [ProtoMember(7)]
        /// <summary>
        /// A text message string.
        /// <br />
        /// Строка текстового(?) сообщения.
        /// </summary>
        public virtual string? Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public RequestMessage()
		{
			//
		}

		public override string ToString() => $"Request: Status {Status}, Sender {Sender ?? Asset.NonAccessable}, Reciever {Reciever ?? Asset.NonAccessable}, Date {Date}, Time {Time}, Message {Message}";
    }
}
