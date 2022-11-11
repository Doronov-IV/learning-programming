using System.ComponentModel;

namespace Toolbox.Flags
{
	/// <summary>
	/// Represents a manual set of flags for denoting application gloabal processing status.
	/// <br />
	/// Представляет собой кастомный набор флагов для определения статуса работы приложения.
	/// </summary>
	public class CustomProcessingStatus : INotifyPropertyChanged
    {


        #region STATE



        /// <inheritdoc cref="IsRunning"/>
        private bool _isRunning;


        /// <inheritdoc cref="IsNotRunning"/>
		private bool _isNotRunning;



        /// <summary>
        /// True - if the process has been launched, otherwise false.
        /// <br />
        /// "True", если процесс был запущен, иначе "false".
        /// </summary>
        public bool IsRunning
		{
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
		}


        /// <summary>
        /// True - if the process has NOT been launched, otherwise false.
        /// <br />
        /// "True", если процесс НЕ был запущен, иначе "false".
        /// </summary>
        public bool IsNotRunning
        {
            get { return _isNotRunning; }
            set
            {
                _isNotRunning = value;
                OnPropertyChanged(nameof(IsNotRunning));
            }
        }



        #endregion STATE





        #region API



        /// <summary>
        /// Toggle flags, changing their values.
        /// <br />
        /// Переключить флаги, заменив их значения.
        /// </summary>
        public void Toggle()
        {
            bool bTemp = IsRunning;
            IsRunning = IsNotRunning;
            IsNotRunning = bTemp;
        }



        #endregion API





        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public CustomProcessingStatus()
        {
            _isRunning = false;
            _isNotRunning = true;
        }


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="isAlreadyRunning">
        /// True - if the application is already running, otherwise false.
        /// <br />
        /// "True", если приложение уже запущено, иначе "false".
        /// </param>
        public CustomProcessingStatus(bool isAlreadyRunning) : this()
        {
            if (isAlreadyRunning)
            {
                _isRunning = true;
                _isNotRunning = false;
            }
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
