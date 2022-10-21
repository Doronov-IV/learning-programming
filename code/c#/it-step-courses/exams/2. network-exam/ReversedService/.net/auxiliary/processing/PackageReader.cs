using System.Data;
using System.Net.Http;
using System.Net.Sockets;
using System.Windows.Interop;
using ReversedService.Net.Auxiliary.Packages;

namespace ReversedService.Net.Auxiliary
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
        /// Read message from TCP-client network stream;
        /// <br />
        /// Считать сообщения из стрима TCP-клиента;
        /// </summary>
        public string ReadMessage()
        {
            string msg = "";
            try
            {
                ///*
                byte[] msgBuffer;
                var length = ReadInt32();
                msgBuffer = new byte[length];
                _NetworkStream.Read(msgBuffer, 0, length);

                msg = Encoding.UTF8.GetString(msgBuffer);
                //*/

                /*
                List<byte> byteList = new();

                while (_NetworkStream.DataAvailable)
                {
                    byteList.Add((byte)_NetworkStream.ReadByte());
                }

                TextMessagePackage package = new TextMessagePackage(byteList.Count);

                package.Data = byteList.ToArray();

                msg = package.Disassemble().Message as string;
                */
            }
            catch
            {
                throw new DataException();
            }
            return msg;
        }


        /// <summary>
        /// Read a file from network stream.
        /// <br />
        /// Прочитать файл из сетевого стрима.
        /// </summary>
        /// <param name="UserName">
        /// A name of reciever-user.
        /// <br />
        /// Имя получателя.
        /// </param>
        /// <returns>
        /// Recieved fileinfo.
        /// <br />
        /// Переданый файл.
        /// </returns>
        public FileInfo ReadFile(string UserName)
        {
            FileInfo info = null;
            try
            {
                byte[] fileBuffer;
                byte[] nameBuffer;

                int fileLength = ReadInt32();
                int nameLength = ReadInt32();

                nameBuffer = new byte[nameLength];
                fileBuffer = new byte[fileLength];

                _NetworkStream.Read(nameBuffer, 0, nameLength);
                var nameString = Encoding.UTF8.GetString(nameBuffer);

                info = new FileInfo($"../../../.files/{UserName} {nameString}");
                _NetworkStream.Read(fileBuffer, 0, fileLength);
                File.WriteAllBytes(info.FullName, fileBuffer);
            }
            catch (Exception)
            {

                throw;
            }
            return info;
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
