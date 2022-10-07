namespace MainNetworkingProject.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel
    {
        public class ServiceWindowViewModelHandler : INotifyPropertyChanged
        {

            private ServiceWindowViewModel _ViewModelRef;

            public ServiceWindowViewModel ViewModelRef
            {
                get { return _ViewModelRef; }
                set
                {
                    _ViewModelRef = value;
                    OnPropertyChanged(nameof(ViewModelRef));
                }
            }


            #region OUTPUT


            public void OnServiceOutput(string sServiceOutput)
            {
                ViewModelRef.State.ServiceLog.Add(sServiceOutput);       
            }


            #endregion OUTPUT



            #region CONSTRUCTION




            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по умолчанию;
            /// </summary>
            public ServiceWindowViewModelHandler()
            {
                ViewModelRef.State.Service.GetServiceOutput += OnServiceOutput;
            }


            /// <summary>
            /// Outer class reference constructor;
            /// <br />
            /// Конструктор для передачи ссылки на внешний класс;
            /// </summary>
            public ServiceWindowViewModelHandler(ServiceWindowViewModel ViewModelReference) : this()
            {
                ViewModelRef = ViewModelReference;
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
