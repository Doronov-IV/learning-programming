using ProtoBuf;

namespace Common.Messages
{
    [ProtoContract]
    public class ResponseMessage
    {


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




        [ProtoMember(1)]
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


        [ProtoMember(2)]
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


        [ProtoMember(3)]
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


        [ProtoMember(4)]
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


        [ProtoMember(5)]
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


        [ProtoMember(6)]
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



        public ResponseMessage()
        {
            _status = RequestStatus.Error;
            _sender = "n/a";
        }

        public ResponseMessage(RequestStatus status, string sender, string reciever, string date, string time, string? message)
        {
            _status = status;
            _sender = sender;
            _reciever = reciever;
            _date = date;
            _time = time;
            _message = message;
        }

        public ResponseMessage(RequestStatus status, string? message)
        {
            _status = status;
            _sender = Asset.NonAccessable;
            _reciever = Asset.NonAccessable;
            _date = Asset.NonAccessable;
            _time = Asset.NonAccessable;
            _message = message;
        }

        public ResponseMessage GetResponseMessage() => new ResponseMessage(Status, Sender, Reciever, Date, Time, Message);

        public override string ToString() => $"Response: Status {Status}, Sender {Sender ?? Asset.NonAccessable}, Reciever {Reciever ?? Asset.NonAccessable}, Date {Date}, Time {Time}, Message {Message}";
	}
}
