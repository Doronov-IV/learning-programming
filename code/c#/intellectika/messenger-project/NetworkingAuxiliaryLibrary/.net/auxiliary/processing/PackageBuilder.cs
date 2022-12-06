using NetworkingAuxiliaryLibrary.Packages;
using System.Text;

namespace NetworkingAuxiliaryLibrary.Processing
{
    /// <summary>
    /// A service that forms packages for communication.
    /// <br />
    /// Сервис, который формирует пакеты для коммуникации.
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
        /// When the service gets message, it serializes and deserializes it and looses the package length. That is why we need this action.
        /// <br />
        /// Когда сервис получает сообщение, он сериализует и десериализует его, теряя длину пакета. Поэтому, нам необходимо это действие.
        /// </summary>
        public void WritePackageLength(MessagePackage package)
        {
            var len = BitConverter.GetBytes(package.Data.Length);
            foreach (var item in len)
            {
                _memoryStream.WriteByte(item);
            }
        }

        
        /// <summary>
        /// Write binary message;
        /// <br />
        /// Записать сообщение в бинарном виде;
        /// </summary>
        /// <param name="package">
        /// Message package;
        /// <br />
        /// Пакет сообщения;
        /// </param>
        public void WriteMessage(MessagePackage package)
        {
            if (package.Assembled)
                _memoryStream.Write(package.Data);
            else throw new Exception("Message has not been assembled!");
        }


        /// <summary>
        /// Write serialized data;
        /// <br />
        /// Записать сериализованные данные;
        /// </summary>
        /// <param name="span">
        /// Binary data;
        /// <br />
        /// Бинарные данные;
        /// </param>
        public void WriteByteSpan(Span<byte> span)
        {
            _memoryStream.Write(BitConverter.GetBytes(span.Length));
            _memoryStream.Write(span);
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
