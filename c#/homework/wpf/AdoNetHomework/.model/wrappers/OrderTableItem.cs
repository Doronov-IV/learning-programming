using AdoNetHomework.Model;

namespace AdoNetHomework.Model.Wrappers
{
    /// <summary>
    /// A wrapper upon class 'Order' to enumerate Customer Number in the table;
    /// <br />
    /// Обёртка класса "Order", чтобы нумеровать ссылку на клиента в коллекции;
    /// </summary>
    public class OrderTableItem : Order, INotifyPropertyChanged
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
        /// Customer table number;
        /// <br />
        /// Номер клиента в таблице-представлении;
        /// </summary>
        private int _CustomerTableNumber;


        /// <summary>
        /// @see private int _CustomerTableNumber;
        /// </summary>
        public int CustomerTableNumber
        {
            get
            {
                return _CustomerTableNumber;
            }

            set
            {
                _CustomerTableNumber = value;
                OnPropertyChanged(nameof(CustomerTableNumber));
            }
        }


        #endregion PROPERTIES




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public OrderTableItem() : base()
        {

        }



        /// <summary>
        /// @see Order - parameter constructor;
        /// </summary>
        public OrderTableItem(int id, int customerId, double summ, DateTime DateTimeNow) : base(id, customerId, summ, DateTimeNow)
        {

        }


        #endregion CONSTRUCTION


    }
}
