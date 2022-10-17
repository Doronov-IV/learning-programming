namespace ReversedService.Net.Auxiliary
{
    /// <summary>
    /// Объект добавляет данные в поток памяти, который используется для получения байтов для отправки на сервер;
    /// По сути, осуществляет передачу данных между клиентом и сервером;
    /// </summary>
    public class PacketBuilder
    {


        #region PROPERTIES


        /// <summary>
        /// A reference to the stream instance (in this case, client stream);
        /// <br />
        /// Ссылка на экземпляр стрима (в данном случае, стим клиента);
        /// </summary>
        MemoryStream _memoryStream;


        #endregion PROPERTIES




        #region API


        /// <summary>
        /// Write operation code to the stream;
        /// <br />
        /// Передать код операции в стрим;
        /// </summary>
        /// <param name="opCode">
        /// Operation code;
        /// <br />
        /// Код операции;
        /// </param>
        public void WriteOpCode(byte opCode)
        {
            _memoryStream.WriteByte(opCode);
        }

        
        /// <summary>
        /// Write binary message;
        /// <br />
        /// Записать сообщение в бинарном виде;
        /// </summary>
        /// <param name="msg">
        /// Message text;
        /// <br />
        /// Текст сообщения;
        /// </param>
        public void WriteMessage(string msg)
        {
            var unicodeMessage = Encoding.UTF8.GetBytes(msg);

            //длинна сообщения
            var msgLenght = unicodeMessage.Length;

            //получение массива байт
            _memoryStream.Write(BitConverter.GetBytes(msgLenght));

            //преобразование символов из сообщения в последовательность байт
            _memoryStream.Write(unicodeMessage);
        }


        /// <summary>
        /// Get all packet bytes including code, message length and the message itself;
        /// <br />
        /// Получить байты пакета, включая код, длину сообщения и само сообщение;
        /// </summary>
        /// <returns>
        /// Packet in a byte array;
        /// <br />
        /// Содержимое пакета в виде массова байтов;
        /// </returns>
        public byte[] GetPacketBytes()
        {
            return _memoryStream.ToArray();
        }


        #endregion API




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public PacketBuilder()
        {
            _memoryStream = new MemoryStream();
        }


        #endregion CONSTRUCTION


    }
}
