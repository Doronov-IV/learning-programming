using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Packages
{
    /// <summary>
    /// Order: sender, reciever, filename, file. Every argument is prefixed with its length.
    /// <br />
    /// Порядок: sender, reciever, filename, file. Каждый параметр начинается с его длины.
    /// </summary>
    public class FileMessagePackage : MessagePackage
    {


        #region PROPERTIES



        #region UNSERIALIZED


        /// <summary>
        /// A file contents.
        /// <br />
        /// Строка сообщения.
        /// </summary>
        public override object? Message
        {
            get { return _message as FileInfo; }
            set { _message = value; }
        }


        /// <inheritdoc cref="FileName"/>
        protected string _fileName;


        /// <summary>
        /// Represents the name (including extention) of the file sent;
        /// <br />
        /// Представляет собой имя (включая расширение) отправляемого файла;
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }


        #endregion UNSERIALIZED



        #region BOOLEAN


        /// <summary>
        /// 'True' if all properties are initialized, otherwise 'false'.
        /// <br />
        /// "True", если свойства инициализированы, иначе "false".
        /// </summary>
        public override bool Initialized
        {
            get { return _sender != string.Empty && _message != null && _fileName != string.Empty && _reciever != string.Empty; }
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
        public override byte[] Assemble()
        {
            byte[] aRes = default;

            if (Initialized)
            {
                List<byte> lRes = new();


                byte[] binSender = Encoding.UTF8.GetBytes(_sender);

                byte[] binReciever = Encoding.UTF8.GetBytes(_reciever);

                byte[] binFileName = Encoding.UTF8.GetBytes(_fileName);

                var fileRef = Message as FileInfo;
                byte[] binMessage = File.ReadAllBytes(fileRef.FullName);


                byte[] binSenderLength = BitConverter.GetBytes(binSender.Length);
                lRes.AddRange(binSenderLength);

                byte[] binRecieverLength = BitConverter.GetBytes(binReciever.Length);
                lRes.AddRange(binRecieverLength);

                byte[] binFileNameLength = BitConverter.GetBytes(binFileName.Length);
                lRes.AddRange(binFileNameLength);

                byte[] binMessageLength = BitConverter.GetBytes(binMessage.Length);
                lRes.AddRange(binMessageLength);


                lRes.AddRange(binSender);
                lRes.AddRange(binReciever);
                lRes.AddRange(binFileName);
                lRes.AddRange(binMessage);

                lRes.InsertRange(0, BitConverter.GetBytes(lRes.Count));

                aRes = lRes.ToArray();

                _Data = aRes;
            }

            return aRes;
        }



        /// <summary>
        /// Do not use this method unless you are okay with default filepath (../../../Downloads).
        /// <br />
        /// Deserialize 'Data'.
        /// <br />
        /// <br />
        /// Не используйте этот метод, если вас не устраивает путь сохранения файла по умолчанию (../../../Downloads).
        /// <br />
        /// Десериализовать "Data".
        /// </summary>
        /// <returns>
        /// A tuple including Sender, Reciever and Message fileinfo;
        /// <br />
        /// Кортеж, включающий в себя отправителя, получателя и сообщение в виде файла;
        /// </returns>
        /// <exception cref="Exception">
        /// Occures in case 'Data' is empy, i.e. message was not assembled.
        /// <br />
        /// Срабатывает если "Data" пустой, т.е. сообщение не было собрано.
        /// </exception>
        public override (string Sender, string Reciever, object Message) Disassemble()
        {
            return Disassemble("../../../Downloads");
        }



        /// <summary>
        /// Deserialize 'Data'.
        /// <br />
        /// Десериализовать "Data".
        /// </summary>
        /// <param name="desiredFileDownloadPath">
        /// A filepath you want to use to save transfered file.
        /// <br />
        /// Путь, который вы бы хотели использовать, для сохранения файла.
        /// </param>
        /// <returns>
        /// A tuple including Sender, Reciever and Message fileinfo;
        /// <br />
        /// Кортеж, включающий в себя отправителя, получателя и сообщение в виде файла;
        /// </returns>
        public (string Sender, string Reciever, object Message) Disassemble(string desiredFileDownloadPath)
        {
            if (_Data != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(_Data))
                {
                    using (BinaryReader binReader = new BinaryReader(memoryStream, Encoding.UTF8, false))
                    {
                        int senderLength = binReader.ReadInt32();

                        int recieverLength = binReader.ReadInt32();

                        int fileNameLength = binReader.ReadInt32();

                        int messageLength = binReader.ReadInt32();


                        byte[] binSender = new byte[senderLength];

                        byte[] binReciever = new byte[recieverLength];

                        byte[] binFileName = new byte[fileNameLength];

                        byte[] binMessage = new byte[messageLength];


                        memoryStream.Read(binSender, 0, senderLength);

                        memoryStream.Read(binReciever, 0, recieverLength);

                        memoryStream.Read(binFileName, 0, fileNameLength);

                        memoryStream.Read(binMessage, 0, messageLength);


                        _sender = Encoding.UTF8.GetString(binSender);

                        _reciever = Encoding.UTF8.GetString(binReciever);

                        _fileName = Encoding.UTF8.GetString(binFileName);

                        // deserializing file;
                        string dirName = $"{desiredFileDownloadPath}";
                        if (!Directory.Exists(Sender)) Directory.CreateDirectory(dirName);

                        string messagePath = $"{dirName}/{_fileName}";
                        _message = new FileInfo(messagePath);
                        File.WriteAllBytes(messagePath, binMessage);
                    }
                }
            }
            else throw new Exception("The 'Data' field was not assigned or was built incorrectly. (File Message Package)");

            return (Sender, Reciever, Message as FileInfo);
        }



        #endregion API





        #region CONSTRUCTION


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="sender">
        /// Message sender nickname.
        /// <br />
        /// Никнейм отправителя сообщения.
        /// </param>
        /// <param name="reciever">
        /// Message reciever nickname.
        /// <br />
        /// Никнейм получателя.
        /// </param>
        /// <param name="message">
        /// Message fileinfo.
        /// <br />
        /// Файл сообщения.
        /// </param>
        public FileMessagePackage(string sender, string reciever, FileInfo message)
        {
            _sender = sender;
            _reciever = reciever;
            _message = message;
            _fileName = message.Name;

            Assemble();
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="Data">
        /// Binary array.
        /// <br />
        /// Массив байтов.
        /// </param>
        public FileMessagePackage(byte[] Data)
        {
            _Data = Data;
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public FileMessagePackage()
        {
            _sender = string.Empty;
            _reciever = string.Empty;
            _message = string.Empty;
            _fileName= string.Empty;

            _Data = null;
        }


        #endregion CONSTRUCTION


    }
}
