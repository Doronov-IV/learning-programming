using ReversedClient.Model;
using ReversedClient.Model.Basics;

namespace ReversedClient.ViewModel.Misc
{
    /// <summary>
    /// An abstraction that helps client handle all chat events.
    /// <br />
    /// Абстракция, которая помогает клиенту обрабатывать все события чатов.
    /// </summary>
    public class MessengerChat : INotifyPropertyChanged
    {


        #region STATE



        /// <inheritdoc cref="MessageList"/>
        private ObservableCollection<string> _messageList;


        /// <inheritdoc cref="Addresser"/>
        private UserModel addresser;


        /// <inheritdoc cref="Addressee"/>
        private UserModel addressee;




        /// <summary>
        /// A list of messages.
        /// <br />
        /// Список сообщений.
        /// </summary>
        public ObservableCollection<string> MessageList
        {
            get { return _messageList; }
            set
            {
                _messageList = value;
                OnPropertyChanged(nameof(MessageList));
            }
        }


        /// <summary>
        /// Addresser user.
        /// <br />
        /// Пользователь-адрессант.
        /// </summary>
        public UserModel Addresser
        {
            get { return addresser; }
            set
            {
                addresser = value;
                OnPropertyChanged(nameof(Addresser));
            }
        }


        /// <summary>
        /// Addressee user.
        /// <br />
        /// Адрессуемый пользователь.
        /// </summary>
        public UserModel Addressee
        {
            get { return addressee; }
            set
            {
                addressee = value;
                OnPropertyChanged(nameof(Addressee));
            }
        }



        #endregion STATE




        #region API



        /// <summary>
        /// Add incomming message.
        /// <br />
        /// Добавить входящее сообщение.
        /// </summary>
        /// <param name="message">
        /// A new mesasge.
        /// <br />
        /// Новое сообщение.
        /// </param>
        public void AddIncommingMessage(string message)
        {
            MessageList.Add($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}] " + $"{Addressee.UserName}: " + message);
        }


        /// <summary>
        /// Add outgoing message.
        /// <br />
        /// Добавить исходящее сообщение.
        /// </summary>
        /// <param name="message">
        /// A new mesasge.
        /// <br />
        /// Новое сообщение.
        /// </param>
        public void AddOutgoingMessage(string message)
        {
            MessageList.Add($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}] " + $"{Addresser.UserName}: " + message);
        }


        /// <summary>
        /// Clear the list of messages.
        /// <br />
        /// Очистить список сообщений.
        /// </summary>
        public void ClearMessages()
        {
            MessageList = new();
        }



        #endregion API




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MessengerChat()
        {
            addressee = null;
            addresser = null;
            _messageList = new();
        }


        /// <summary>
        /// Parametrised constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="addresser">
        /// An addresser reference.
        /// <br />
        /// Ссылка на адрессанта.
        /// </param>
        /// <param name="addressee">
        /// An addressee reference.
        /// <br />
        /// Ссылка на адрессуемого.
        /// </param>
        public MessengerChat(UserModel addresser, UserModel addressee) : this()
        {
            this.addresser = addresser;
            this.addressee = addressee;
        }



        #region Property changed


        /// <summary>
        /// Propery changed event handler;
        /// <br />
        /// Делегат-обработчик события 'property changed';
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Handler-method of the 'property changed' delegate;
        /// <br />
        /// Метод-обработчик делегата 'property changed';
        /// </summary>
        /// <param name="propName">The name of the property;<br />Имя свойства;</param>
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #endregion Property changed



        #endregion CONSTRUCTION


    }
}
