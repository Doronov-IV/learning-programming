using MainNetworkingProject.ViewModel.Handlers;
using MainNetworkingProject.ViewModel.States;
using System.ComponentModel;

namespace MainNetworkingProject.ViewModel
{
    /// <summary>
    /// ;
    /// <br />
    /// ;
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        #region PROPERTIES


        /// <summary>
        /// State of the viewmodel;
        /// <br />
        /// Состояние вьюмодели;
        /// </summary>
        private MainWindowViewModelState _State;


        /// <summary>
        /// Handler of the viewmodel;
        /// <br />
        /// Хендлер вьюмодели;
        /// </summary>
        private MainWindowViewModelHandler _Handler;



        public MainWindowViewModelState State
        {
            get { return _State; }
            set
            {
                _State = value;
                OnPropertyChanged(nameof(State));
            }
        }


        #endregion PROPERTIES




        #region COMMANDS







        #endregion COMMANDS




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {

        }


        #endregion CONSTRUCTION


    }
}
