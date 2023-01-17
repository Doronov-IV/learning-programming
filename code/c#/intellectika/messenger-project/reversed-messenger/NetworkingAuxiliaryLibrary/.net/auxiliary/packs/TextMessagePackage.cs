using System.Text;

namespace NetworkingAuxiliaryLibrary.Packages
{
    /// <summary>
    /// A string to binary text message compiler.
    /// <br />
    /// The order is: Sender, Reciever, Message.
    /// <br />
    /// <br />
    /// Конвертер текстовых сообщений в бинарный вид.
    /// <br />
    /// Порядок: Sender, Reciever, Message.
    /// </summary>
    public class TextMessagePackage : MessagePackage
    {


        #region PROPERTIES



        #region UNSERIALIZED


        /// <summary>
        /// A text message string.
        /// <br />
        /// Строка текстового(?) сообщения.
        /// </summary>
        public override object? Message
        {
            get { return _message as string; }
            set { _message = value; }
        }


        #endregion UNSERIALIZED



        #region BOOLEAN



        /// <summary>
        /// 'True' if all properties are initialized, otherwise 'false'.
        /// <br />
        /// "True", если свойства инициализированы, иначе "false".
        /// </summary>
        public override bool Initialized
        {
            get { return _sender != string.Empty && (_message != null && _message as string != string.Empty) && _reciever != string.Empty && _date != string.Empty && _time != string.Empty; }
        }



        #endregion BOOLEAN



        #endregion PROPERTIES





        #region API


        /// <summary>
        /// Serialize gathered information.
        /// <br />
        /// Сериализовать собраную информацию.
        /// </summary>
        /// <returns>
        /// A fully-assembled array of bytes.
        /// <br />
        /// Полностью собранный масств байтов.
        /// </returns>
        public override void Assemble()
        { 
            if (Initialized)
            {
                List<byte> lRes = new();


                byte[] binSender = Encoding.UTF8.GetBytes(_sender);

                byte[] binReciever = Encoding.UTF8.GetBytes(_reciever);

                byte[] binDate = Encoding.UTF8.GetBytes(_date);

                byte[] binTime  = Encoding.UTF8.GetBytes(_time);

                byte[] binMessage = Encoding.UTF8.GetBytes(_message as string);


                byte[] binSenderLength = BitConverter.GetBytes(binSender.Length);

                byte[] binRecieverLength = BitConverter.GetBytes(binReciever.Length);

                byte[] binDateLength = BitConverter.GetBytes(binDate.Length);

                byte[] binTimeLength = BitConverter.GetBytes(binTime.Length);

                byte[] binMessageLength = BitConverter.GetBytes(binMessage.Length);


                lRes.AddRange(binSenderLength);

                lRes.AddRange(binRecieverLength);

                lRes.AddRange(binDateLength);

                lRes.AddRange(binTimeLength);

                lRes.AddRange(binMessageLength);

                lRes.AddRange(binSender);

                lRes.AddRange(binReciever);

                lRes.AddRange(binDate);

                lRes.AddRange(binTime);

                lRes.AddRange(binMessage);

                Int32 tempDebugSize = new Int32();
                tempDebugSize = lRes.Count;
                int tempDebugSizeSerializationInfo2 = 0;

                byte[] tempDebugSizeSerializationInfo1 = (BitConverter.GetBytes(lRes.Count));
                tempDebugSizeSerializationInfo2 = BitConverter.ToInt32(tempDebugSizeSerializationInfo1);
                lRes.InsertRange(0, BitConverter.GetBytes(tempDebugSizeSerializationInfo2));

                _Data = lRes.ToArray();

                lRes.Clear();

                lRes = null;
            }
        }



        /// <summary>
        /// Deserialize byte array, provided by 'Data' property.
        /// <br />
        /// Десериализовать массив байтов, представленный свойством "Data".
        /// </summary>
        public override MessagePackage Disassemble()
        {
            if (_Data != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(_Data))
                {
                    using (BinaryReader binReader = new BinaryReader(memoryStream, Encoding.UTF8, false))
                    {
                        int senderLength = binReader.ReadInt32();

                        int recieverLength = binReader.ReadInt32();

                        int dateLength = binReader.ReadInt32();

                        int timeLength = binReader.ReadInt32();

                        int messageLength = binReader.ReadInt32();


                        byte[] binSender = new byte[senderLength];

                        byte[] binReciever = new byte[recieverLength];

                        byte[] binDate = new byte[dateLength];

                        byte[] binTime = new byte[timeLength];

                        byte[] binMessage = new byte[messageLength];


                        memoryStream.Read(binSender, 0, senderLength);

                        memoryStream.Read(binReciever, 0, recieverLength);

                        memoryStream.Read(binDate, 0, dateLength);

                        memoryStream.Read(binTime, 0, timeLength);

                        memoryStream.Read(binMessage, 0, messageLength);


                        _sender = Encoding.UTF8.GetString(binSender);

                        _reciever = Encoding.UTF8.GetString(binReciever);

                        _date = Encoding.UTF8.GetString(binDate);

                        _time= Encoding.UTF8.GetString(binTime);

                        _message = Encoding.UTF8.GetString(binMessage);
                    }
                }
            }
            else throw new Exception("The 'Data' field was not assigned or was built incorrectly. (Text Message Package)");

            return new TextMessagePackage(Sender, Reciever, Date, Time, Message as string);
        }


        #endregion API





        #region IMessage


        public string GetSender()
        {
            return _sender;
        }


        public string GetReciever()
        {
            return _reciever;
        }


        public string GetDate()
        {
            return _date;
        }


        public string GetTime()
        {
            return _time;
        }


        public object GetMessage()
        {
            return _message;
        }


        #endregion IMessage





        #region CONSTRUCTION


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="sender">
        /// Message sender nickname.
        /// <br />
        /// Никнейм отправителя сообщения.
        /// </param>
        /// <param name="reciever">
        /// Message tempRecieverReference nickname.
        /// <br />
        /// Никнейм получателя.
        /// </param>
        /// <param name="date">
        /// Date of sending.
        /// <br />
        /// Дата отправки.
        /// </param>
        /// <param name="time">
        /// Time of sendign.
        /// <br />
        /// Время отправки.
        /// </param>
        /// <param name="message">
        /// Message text.
        /// <br />
        /// Текст сообщения.
        /// </param>
        public TextMessagePackage(string sender, string reciever, string date, string time, string message)
        {
            _sender = sender;
            _reciever = reciever;
            _date = date;
            _time = time;
            _message = message;

            Assemble();
        }

        

        /// <summary>
        /// Constructor with the length of the stream.
        /// <br />
        /// Конструктор с размером стрима.
        /// </summary>
        /// <param name="streamLength">
        /// The number of bytes in the stream.
        /// <br />
        /// Число байтов в стриме.
        /// </param>
        public TextMessagePackage(byte[] Data)
        {
            _Data = Data;

            try
            {
                var temp = Disassemble();
                _sender = temp.Sender;
                _reciever = temp.Reciever;
                _message = temp.Message;
                _date = temp.Date;
                _time = temp.Time;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception during text message deserialization. Details: {ex.Message}");
            }
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TextMessagePackage()
        {
            _sender = string.Empty;
            _reciever = string.Empty;
            _message = string.Empty;
            _date = string.Empty;
            _time = string.Empty;

            _Data = null;
        }


        #endregion CONSTRUCTION


    }
}
