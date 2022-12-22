using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Packages
{
    /// <summary>
    /// An entity providing basic logic for message;
    /// <br />
    /// Сущность, предоставляющая базовую логику сообщений;
    /// </summary>
    public abstract class MessagePackage
    {


        #region PROPERTIES




        #region UNSERIALIZED



        /// <inheritdoc cref="Sender"/>
        protected string _sender;


        /// <inheritdoc cref="Reciever"/>
        protected string _reciever;


        /// <inheritdoc cref="Date"/>
        protected string _date;


        /// <inheritdoc cref="Time"/>
        protected string _time;


        /// <inheritdoc cref="Message"/>
        protected object? _message;



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


        /// <summary>
        /// The message sending date.
        /// <br />
        /// Дата отправки сообщения.
        /// </summary>
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }


        /// <summary>
        /// The message sending time.
        /// <br />
        /// Время отправки сообщения.
        /// </summary>
        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }


        /// <summary>
        /// A text message string.
        /// <br />
        /// Строка текстового(?) сообщения.
        /// </summary>
        public virtual object? Message
        {
            get { return _message; }
            set { _message = value; }
        }



        #endregion UNSERIALIZED



        #region BINARY


        /// <inheritdoc cref="Data"/>
        protected byte[]? _Data;


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
        public virtual bool Initialized
        {
            get { return _sender != string.Empty && _message != null && _reciever != string.Empty; }
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
        public abstract void Assemble();



        /// <summary>
        /// Deserialize byte array, provided by 'Data' property.
        /// <br />
        /// Десериализовать массив байтов, представленный свойством "Data".
        /// </summary>
        public abstract MessagePackage Disassemble();


        #endregion API





        #region CONSTRUCTION



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="sender">
        /// Sender nickname.
        /// <br />
        /// Никнейм отправителя.
        /// </param>
        /// <param name="reciever">
        /// Reciever nickname.
        /// <br />
        /// Никнейм получателя.
        /// </param>
        /// <param name="message">
        /// Message object.
        /// <br />
        /// Объект сообщения.
        /// </param>
        public MessagePackage(string sender, string reciever, object message)
        {
            _sender = sender;
            _reciever = reciever;
            _message = message;

            Assemble();
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MessagePackage()
        {
            _sender = string.Empty;
            _reciever = string.Empty;
            _message = string.Empty;

            _Data = null;
        }



        #endregion CONSTRUCTION


    }
}
