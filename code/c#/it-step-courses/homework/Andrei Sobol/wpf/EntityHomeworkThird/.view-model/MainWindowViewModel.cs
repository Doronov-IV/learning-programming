namespace EntityHomeworkThird.ViewModel
{
    public class MainWindowViewModel
    {


        #region PROPERTIES


        /// <summary>
        /// Handler is a file with all event handler methods;
        /// <br />
        /// Handler - это файл со всеми методами обработки событий;
        /// </summary>
        private MainWindowViewModelHandler _Handler;


        #endregion PROPERTIES





        #region API


        //


        #endregion API





        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            _Handler = new(this);
        }


        #endregion CONSTRUCTION

    }
}
