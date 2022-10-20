using System.Text;

namespace Range
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
    public class TextMessagePackage
    {


        #region PROPERTIES



        #region TEXT


        /// <inheritdoc cref="Sender"/>
        private string _sender;

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



        /// <inheritdoc cref="Reciever"/>
        private string _reciever;

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


        /// <inheritdoc cref="Message"/>
        private string _Message;

        /// <summary>
        /// A text message string.
        /// <br />
        /// Строка текстового(?) сообщения.
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }


        #endregion TEXT



        #region BINARY


        /// <inheritdoc cref="Data"/>
        private byte[]? _Data;


        /// <summary>
        /// A raw array of bytes.
        /// <br />
        /// Простой массив байтов.
        /// </summary>
        public byte[]? Data
        {
            get { return _Data; }
            set { _Data = value; }
        }


        #endregion BINARY



        #region BOOLEAN



        /// <summary>
        /// 'True' if 'Data' is not null, otherwise 'false'.
        /// <br />
        /// "True", если Data не "null", иначе "false".
        /// </summary>
        public bool Assembled
        {
            get { return _Data != null; }
        }


        /// <summary>
        /// 'True' if all properties are initialized, otherwise 'false'.
        /// <br />
        /// "True", если свойства инициализированы, иначе "false".
        /// </summary>
        public bool Initialized
        {
            get { return _sender != string.Empty && _Message != string.Empty && _reciever != string.Empty; }
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
        public byte[] Assemble()
        {
            List<byte> lRes = new();


            byte[] binSender = Encoding.UTF8.GetBytes(_sender);

            byte[] binReciever = Encoding.UTF8.GetBytes(_reciever);

            byte[] binMessage = Encoding.UTF8.GetBytes(_Message);


            byte[] binSenderLength = BitConverter.GetBytes(binSender.Length);
            lRes.AddRange(binSenderLength);

            byte[] binRecieverLength = BitConverter.GetBytes(binReciever.Length);
            lRes.AddRange(binRecieverLength);

            byte[] binMessageLength = BitConverter.GetBytes(binMessage.Length);
            lRes.AddRange(binMessageLength);


            lRes.AddRange(binSender);
            lRes.AddRange(binReciever);
            lRes.AddRange(binMessage);

            byte[] aRes = lRes.ToArray();

            _Data = aRes;

            return aRes;
        }



        /// <summary>
        /// Deserialize byte array, provided by 'Data' property.
        /// <br />
        /// Десериализовать массив байтов, представленный свойством "Data".
        /// </summary>
        public void Disassemble()
        {
            if (_Data != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(_Data))
                {
                    using (BinaryReader binReader = new BinaryReader(memoryStream, Encoding.UTF8, false))
                    {
                        int senderLength = binReader.ReadInt32();

                        int recieverLength = binReader.ReadInt32();

                        int messageLength = binReader.ReadInt32();


                        byte[] binSender = new byte[senderLength];

                        byte[] binReciever = new byte[recieverLength];

                        byte[] binMessage = new byte[messageLength];


                        memoryStream.Read(binSender, 0, senderLength);

                        memoryStream.Read(binReciever, 0, recieverLength);

                        memoryStream.Read(binMessage, 0, messageLength);


                        _sender = Encoding.UTF8.GetString(binSender);

                        _reciever = Encoding.UTF8.GetString(binReciever);

                        _Message = Encoding.UTF8.GetString(binMessage);
                    }
                }
            }
            else throw new Exception("The 'Data' field was not assigned or was built incorrectly. (Text Message Package)");
        }


        #endregion API





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
        /// Message reciever nickname.
        /// <br />
        /// Никнейм получателя.
        /// </param>
        /// <param name="message">
        /// Message text.
        /// <br />
        /// Текст сообщения.
        /// </param>
        public TextMessagePackage(string sender, string reciever, string message)
        {
            _sender = sender;
            _reciever = reciever;
            _Message = message;

            Assemble();
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TextMessagePackage()
        {
            _Data = null;
        }


        #endregion CONSTRUCTION


    }
}
