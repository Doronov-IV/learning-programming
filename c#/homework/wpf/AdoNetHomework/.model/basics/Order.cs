

namespace AdoNetHomework.Model
{
    /// <summary>
    /// Represents an object from table 'Orders';
    /// <br />
    /// Представляет собой один объект из таблицы "Orders";
    /// </summary>
    public class Order : INotifyPropertyChanged
    {


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



        #region PROPERTIES - forming the State of an Object



        #region Private references


        /// <summary>
        /// 'Orders' table primary key id;
        /// <br />
        /// Идентификатор, первичный ключ для таблицы 'Orders';
        /// </summary>
        protected int _Id;


        /// <summary>
        /// 'Orders' table customers' id;
        /// <br />
        /// Идентификатор клиентов для таблицы 'Orders';
        /// </summary>
        protected int _CustomerId;


        /// <summary>
        /// 'Orders' table order price value;
        /// <br />
        /// Показатель суммы заказа для таблицы 'Orders';
        /// </summary>
        protected double _Summ;


        /// <summary>
        /// 'Orders' table date reference;
        /// <br />
        /// Заметка о дате заказа для таблицы 'Orders';
        /// </summary>
        protected string _Date;


        #endregion Private references




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



        #endregion PROPERTIES - forming the State of an Object



        #region CONSTRUCTION - Object Lifetime Control


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Order()
        {
            Id = int.MaxValue;
            CustomerId = int.MaxValue;
            Summ = double.MaxValue;
            
            // it sort of parses 'DateTime.Now' into 'DateOnly'-like format;
            Date = DateTime.Now.ToString();
        }




        #region OVERLOADING - Parameters Overloading


        /// <summary>
        /// @see Order(int, int, double, DateOnly) in this file right above this method;
        /// <br />
        /// It's a copy of the previous one but with 'DateTime.Now' parameter;
        /// <br />
        /// Это просто копия предыдущего, только с параметром "DateTime.Now";
        /// </summary>
        public Order(int id, int customerId, double summ, DateTime DateTimeNow)
        {
            Id = id;
            CustomerId = customerId;
            Summ = summ;
            Date = DateTime.Now.ToString();
        }


        /// <summary>
        /// @see Order(int, int, double, DateOnly) in this file right above this method;
        /// <br />
        /// It's a copy of the previous one but with 'DateTime.Now' parameter;
        /// <br />
        /// Это просто копия предыдущего, только с параметром "DateTime.Now";
        /// </summary>
        public Order(int id, int customerId, double summ, string dateString)
        {
            Id = id;
            CustomerId = customerId;
            Summ = summ;
            Date = dateString;
        }


        #endregion OVERLOADING - Parameters Overloading




        #endregion CONSTRUCTION - Object Lifetime Control


    }
}
