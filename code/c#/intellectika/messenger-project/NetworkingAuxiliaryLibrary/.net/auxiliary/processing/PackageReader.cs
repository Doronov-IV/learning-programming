using NetworkingAuxiliaryLibrary.Packages;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace NetworkingAuxiliaryLibrary.Processing
{
    /// <summary>
    /// An auxiliary object that helps reading data from streams;
    /// <br />
    /// Вспомогательный объект, который помогает читать данные из стримов;
    /// </summary>
    public class PackageReader : BinaryReader
    {


        #region PROPERTIES



        /// <summary>
        /// TCP-client's stream;
        /// <br />
        /// Стрим ТСР-клиента;
        /// </summary>
        private NetworkStream _NetworkStream;



        #endregion PROPERTIES





        #region API



        /// <summary>
        /// Read bytes from the stream and return raw array of bytes.
        /// <br />
        /// Считать байты из стрима и вернуть массив из этих байтов.
        /// </summary>
        /// <returns>
        /// The array of bytes (that then have to be assigned to the corresponding package and dissasembled).
        /// <br />
        /// Массив байтов (который затем должен быть присвоен соответствующему пакету и расшифрован).
        /// </returns>
        /// <exception cref="DataException">
        /// An error that occures when package sends wrong message length was passed to the stream.
        /// <br />
        /// Ошибка, которая возникает в случае, когда пакетом в стрим была передана неправильная длина сообщения.
        /// </exception>
        private byte[] ReadMessageBytes()
        {
            byte[] bRes = null;
            try
            {
                List<byte> byteList = new();

                int packageLength = ReadInt32();

                int i = 0;
                while (i++ < packageLength)
                {
                    byteList.Add((byte)_NetworkStream.ReadByte());
                }

                bRes = byteList.ToArray();

                byteList.Clear();
            }
            catch 
            {
                throw new DataException();
            }

            return bRes;
        }


        /// <summary>
        /// Read (text) message from the network stream.
        /// <br />
        /// Считать сообщение (текстовое) из сетевого стрима.
        /// </summary>
        /// <returns>
        /// The text of the message in a line.
        /// <br />
        /// Текст сообщения в виде строки.
        /// </returns>
        public MessagePackage ReadMessage()
        {
            byte[] tempArray = ReadMessageBytes();

            TextMessagePackage package = new TextMessagePackage(tempArray);

            return package;
        }

        
        /// <summary>
        /// Read file from the network stream.
        /// <br />
        /// Считать файл из сетевого стрима.
        /// </summary>
        /// <param name="UserName">
        /// Name of the reciever.
        /// <br />
        /// Имя получателя.
        /// </param>
        /// <returns>
        /// The info of the file read.
        /// <br />
        /// Информация о считанном файле.
        /// </returns>
        public MessagePackage ReadFile(string UserName)
        {
            byte[] tempArray = ReadMessageBytes();

            FileMessagePackage package = new FileMessagePackage(tempArray);

            return package;
        }



        #endregion API





        #region CONSTRUCTION



        /// <summary>
        /// Parametrised constructor;
        /// <br />
        /// Параметризованный конструктор;
        /// </summary>
        /// <param name="networkStream">
        /// An instance of the tcp-client's stream to use base class constructor;
        /// <br />
        /// Экземпляр стрима TCP-клиента, чтобы воспользоваться конструктором базового класса;
        /// </param>
        public PackageReader(NetworkStream networkStream) : base(networkStream)
        {
            _NetworkStream = networkStream;
        }



        #endregion CONSTRUCTION


    }
}
