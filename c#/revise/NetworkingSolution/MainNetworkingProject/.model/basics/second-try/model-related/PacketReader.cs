namespace MainNetworkingProject.Model.Basics
{
    public class PacketReader : BinaryReader
    {


        #region PROPERTIES


        private NetworkStream _NetworkStream;


        #endregion PROPERTIES





        #region API


        public string ReadMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            _NetworkStream.Read(msgBuffer, 0, length);

            var msg = Encoding.ASCII.GetString(msgBuffer);
            return msg;
        }


        #endregion API





        #region CONSTRUCTION


        public PacketReader(NetworkStream networkStream) : base(networkStream)
        {
            _NetworkStream = networkStream;
        }


        #endregion CONSTRUCTION


    }
}
