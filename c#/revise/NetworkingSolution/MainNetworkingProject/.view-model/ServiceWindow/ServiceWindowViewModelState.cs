using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel
    {
        public class ServiceWindowViewModelState : INotifyPropertyChanged
        {

            private ExplorerService _Service;

            public ExplorerService Service
            {
                get { return _Service; }
                set 
                {
                    _Service = value;
                    OnPropertyChanged(nameof(Service));
                }
            }

            private List<string> _ServiceLog;

            public List<string> ServiceLog
            {
                get { return _ServiceLog; }
                set
                {
                    _ServiceLog = value;
                    OnPropertyChanged(nameof(ServiceLog));
                }
            }


            #region CONSTRUCTION




            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по умолчанию;
            /// </summary>
            public ServiceWindowViewModelState()
            {

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
}
