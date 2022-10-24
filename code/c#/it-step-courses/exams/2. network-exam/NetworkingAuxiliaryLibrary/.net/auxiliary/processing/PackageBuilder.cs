using NetworkingAuxiliaryLibrary.Packages;
using System.Text;

namespace NetworkingAuxiliaryLibrary.Processing
{
    /// <summary>
    /// Объект добавляет данные в поток памяти, который используется для получения байтов для отправки на сервер;
    /// По сути, осуществляет передачу данных между клиентом и сервером;
    /// </summary>
    public class PackageBuilder
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
            // the most important thing is: to code message to utf BEFORE we calculate its length;

            ///*
            TextMessagePackage package = new TextMessagePackage("place_holder", "place_holder", msg);

            _memoryStream.Write(package.Data);
            //*/


            /*
            var unicodeMessage = Encoding.UTF8.GetBytes(msg);

            var msgLenght = unicodeMessage.Length;

            _memoryStream.Write(BitConverter.GetBytes(msgLenght));

            _memoryStream.Write(unicodeMessage);
            */
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
        public void WriteFile(FileInfo info)
        {
            if (info != null)
            {
                FileMessagePackage package = new FileMessagePackage("place_holder", "place_holder", info);

                _memoryStream.Write(package.Data);

                /* var binFile = File.ReadAllBytes(info.FullName);

                string sFileName = info.Name;

                var binFileName = Encoding.UTF8.GetBytes(sFileName);

                // lengths

                var fileNameLen = BitConverter.GetBytes(binFileName.Length);

                var binFileLen = BitConverter.GetBytes(binFile.Length);

                _memoryStream.Write(binFileLen);

                _memoryStream.Write(fileNameLen);

                // strings

                _memoryStream.Write(binFileName);

                _memoryStream.Write(binFile);
                */
            }
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
        public PackageBuilder()
        {
            _memoryStream = new MemoryStream();
        }


        #endregion CONSTRUCTION


    }
}
