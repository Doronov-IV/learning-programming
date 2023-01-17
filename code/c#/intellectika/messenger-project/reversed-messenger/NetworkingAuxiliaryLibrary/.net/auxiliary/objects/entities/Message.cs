using NetworkingAuxiliaryLibrary.Packages;
using Tools.Formatting;
using Tools.Toolbox;

namespace NetworkingAuxiliaryLibrary.Objects.Entities
{
    public class Message : IMessage
    {

        public int Id { get; set; }


        /// <summary>
        /// Contents of the message.
        /// <br />
        /// Содержимое сообщения.
        /// </summary>
        public string Contents { get; set; }


        public int AuthorId { get; set; }


        public string Date { get; set; } = null!;


        public string Time { get; set; } = null!;


        public User Author { get; set; } = null!;


        public Chat Chat { get; set; } = null!;


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Message()
        {

        }



        #region IMessage


        public string GetSender()
        {
            return Author.PublicId;
        }


        public string GetReciever()
        {
            throw new NotImplementedException();
            return string.Empty;
        }


        public string GetDate()
        {
            return Date;
        }


        public string GetTime()
        {
            return Time;
        }


        public object GetMessage()
        {
            return Contents;
        }


        #endregion IMessage

    }
}
