

namespace AdoNetHomework.Model
{
    /// <summary>
    /// Represents one object from table 'Users';
    /// <br />
    /// Представляет собой один объект из таблицы "Users";
    /// </summary>
    public class User : INotifyPropertyChanged
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



        #region PROPERTIES - forming the State of an Object


        private int _Id;

        private string _Name;

        private string _PhoneNumber;

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

        public string Name 
        {
            get 
            {
                return _Name;
            }
            set 
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string PhoneNumber 
        {
            get 
            {
                return _PhoneNumber; 
            }
            set 
            {
                _PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }


        #endregion PROPERTIES - forming the State of an Object



        #region CONSTRUCTION - Object Lifetime Control


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public User()
        {
            this.Id = int.MaxValue;
            this.Name = "";
            this.PhoneNumber = "";
        }


        /// <summary>
        /// Constructor with parameters;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="Id">Id;<br />Id;</param>
        /// <param name="Name">Name;<br />Имя;</param>
        /// <param name="PhoneNumber">Phone number;<br />Номер телефона;</param>
        public User(int Id, string Name, string PhoneNumber) : this()
        {
            this.Id = Id;
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
        }


        #endregion CONSTRUCTION - Object Lifetime Control

    }
}
