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




        #region Public properties


        /// <summary>
        /// @see private int _Id in this file in 'Private references' region;
        /// </summary>
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        /// <summary>
        /// @see private int _CustomerId in this file in 'Private references' region;
        /// </summary>
        public int CustomerId
        {
            get
            {
                return
                    _CustomerId;
            }
            set
            {
                _CustomerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }


        /// <summary>
        /// @see private double _Summ in this file in 'Private references' region;
        /// </summary>
        public double Summ
        {
            get
            {
                return
                    _Summ;
            }
            set
            {
                _Summ = value;
                OnPropertyChanged(nameof(Summ));
            }
        }


        /// <summary>
        /// @see private DateOnly _Date in this file in 'Private references' region;
        /// </summary>
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


        #endregion Public properties



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
        public OrderTableItem(int id, int customerId, double summ, string dateString) : base(id, customerId, summ, dateString)
        {

        }


        #endregion CONSTRUCTION


    }
}
