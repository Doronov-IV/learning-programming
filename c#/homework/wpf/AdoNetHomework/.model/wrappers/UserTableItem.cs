
using AdoNetHomework.Model;

namespace AdoNetHomework.Model.Wrappers
{
    /// <summary>
    /// A wrapper upon class 'User' to enumerate it in the table;
    /// <br />
    /// Обёртка класса "User", чтобы нумеровать его в коллекции;
    /// </summary>
    public class UserTableItem : User, INotifyPropertyChanged
    {


        #region Property changed legacy


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


        #endregion Property changed legacy




        #region PROPERTIES


        /// <summary>
        /// The ordered number in the Users-View table;
        /// <br />
        /// Порядковый номер во view-таблице "Users";
        /// </summary>
        private int _TableNumber = 0;


        /// <summary>
        /// @see private int _TableNumber;
        /// </summary>
        public int TableNumber
        {
            get
            {
                return _TableNumber;
            }

            set
            {
                _TableNumber = value;
                OnPropertyChanged(nameof(TableNumber));
            }
        }


        #endregion PROPERTIES




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public UserTableItem() : base()
        {
        }


        /// <summary>
        /// @see Order - parameter constructor;
        /// </summary>
        public UserTableItem(int Id, string Name, string PhoneNumber) : base(Id, Name, PhoneNumber)
        {
        }


        #endregion CONSTRUCTION


    }
}
