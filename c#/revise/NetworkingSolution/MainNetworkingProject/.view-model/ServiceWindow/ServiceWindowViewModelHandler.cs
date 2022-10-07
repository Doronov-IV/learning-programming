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
                ViewModelRef.Text = sServiceOutput;
                ViewModelRef.ServiceLog.Add(sServiceOutput);       
            }


            #endregion OUTPUT



            #region CONTROLS


            public void OnRunButtonClick()
            {
                Task.Run(() =>
                {
                    ViewModelRef.State.Service.Run();
                });
            }


            #endregion CONTROLS



            #region CONSTRUCTION




            /// <summary>
            /// Outer class reference constructor;
            /// <br />
            /// Конструктор для передачи ссылки на внешний класс;
            /// </summary>
            public ServiceWindowViewModelHandler(ServiceWindowViewModel ViewModelReference)
            {
                //ViewModelRef = ViewModelReference;

                //ViewModelRef.State.Service.GetServiceOutput += OnServiceOutput;
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
