namespace EntityHomeworkThird.ViewModel
{
    public class MainWindowViewModelHandler
    {


        #region PROPERTIES



        /// <summary>
        /// ViewModel reference;
        /// <br />
        /// Ссылка на вью-модель;
        /// </summary>
        private MainWindowViewModel _ViewModelReference;



        #endregion PROPERTIES





        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModelHandler(MainWindowViewModel ViewModelReference)
        {
            _ViewModelReference = ViewModelReference;
        }



        #endregion CONSTRUCTION


    }
}
