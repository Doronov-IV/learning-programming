namespace MainNetworkingProject.Model.Basics
{
    /// <summary>
    /// Объект добавляет данные в поток памяти, который используется для получения байтов для отправки на сервер;
    /// По сути, осуществляет передачу данных между клиентом и сервером;
    /// </summary>
    public class PacketBuilder
    {


        #region PROPERTIES


        /// <summary>
        /// Поток, резервным хранилищем которого является память;
        /// </summary>
        MemoryStream _memoryStream;


        #endregion PROPERTIES




        #region API


        /// <summary>
        /// Записывает байт в текущее положение текущего потока;
        /// </summary>
        /// <param name="opCode"></param>
        public void WriteOpCode(byte opCode)
        {
            _memoryStream.WriteByte(opCode);
        }

        /// <summary>
        /// Write(ReadOnlySpan<Byte>): Записать последовательность байтов, содержащихся в source, 
        /// в текущий поток в памяти и перемещает текущую позицию внутри этого потока в памяти на число записанных байтов;
        /// 
        /// 'BitConverter': Преобразует базовые типы данных в массив байтов, а массив байтов — в базовые типы данных;
        /// </summary>
        /// <param name="msg">Текст ообщения</param>
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
        /// получение массива байт пакета
        /// </summary>
        /// <returns>Массив байт</returns>
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
