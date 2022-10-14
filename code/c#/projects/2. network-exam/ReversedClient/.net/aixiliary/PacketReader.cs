using System.Windows.Interop;

namespace ReversedClient.Net.Auxiliary
{
    /// <summary>
    /// An auxiliary object that helps reading data from streams;
    /// <br />
    /// Вспомогательный объект, который помогает читать данные из стримов;
    /// </summary>
    public class PacketReader : BinaryReader
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
                byte[] msgBuffer;
                var length = ReadInt32();
                msgBuffer = new byte[length];
                _NetworkStream.Read(msgBuffer, 0, length);

                msg = Encoding.UTF8.GetString(msgBuffer);
            }
            catch
            {

            }
            return msg;
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
        public PacketReader(NetworkStream networkStream) : base(networkStream)
        {
            _NetworkStream = networkStream;
        }



        #endregion CONSTRUCTION


    }
}
