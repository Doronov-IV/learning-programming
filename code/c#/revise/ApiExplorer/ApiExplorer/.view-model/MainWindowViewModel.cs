using ApiExplorer;

namespace ApiExplorer.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES



        private HttpClient _Client;

        private ObservableCollection<string> _ListOfProperties;

        public ObservableCollection<string> ListOfProperties
        {
            get { return _ListOfProperties; }
            set
            {
                _ListOfProperties = value;
                OnPropertyChanged(nameof(ListOfProperties));
            }
        }



        #endregion PROPERTIES




        #region COMMANDS


        public DelegateCommand LoadCommand { get; }


        #endregion COMMANDS




        #region API


        private void FillPropertiesList()
        {
            MethodInfo[] info = typeof(HttpClient).GetMethods();

            StringBuilder builder = new();

            foreach(var el in info)
            {
                builder = new();
                builder.Append("(");
                foreach(var param in el.GetParameters())
                {
                    builder.Append(param.Name + ", ");
                }
                if (builder.Length > 2) builder[builder.Length - 2] = ')';
                ListOfProperties.Add(el.Name + builder.ToString().Trim());
            }
        }


        #endregion API




        #region CONSTRUCTION




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




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            _Client = new();
            _ListOfProperties = new();

            LoadCommand = new(FillPropertiesList);
        }


        #endregion CONSTRUCTION

    }
}
