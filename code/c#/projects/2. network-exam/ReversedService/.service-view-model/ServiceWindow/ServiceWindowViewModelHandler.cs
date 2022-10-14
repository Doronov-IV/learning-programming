using ReversedService.Net.Main;
using System.Threading;

namespace ReversedService.ViewModel.ServiceWindow
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
                ViewModelRef.ServiceLog.Add(sServiceOutput);       
            }


            #endregion OUTPUT



            #region CONTROLS


            public void OnRunButtonClick()
            {
                Thread runninThread = new Thread(ViewModelRef.Service.Run);
                runninThread.Start();
                ToggleConnectionState();
            }



            public void OnCancelServiceButtonClick()
            {
                Process.GetProcesses().ToList().Find(n => n.ProcessName == "ReversedService")?.Kill();
            }


            #endregion CONTROLS



            #region LOGIC


            private void ToggleConnectionState()
            {
                bool bTemp = _ViewModelRef.IsRunning;
                _ViewModelRef.IsRunning = _ViewModelRef.IsNotRunning;
                _ViewModelRef.IsNotRunning = bTemp;
            }


            #endregion LOGIC



            #region CONSTRUCTION




            /// <summary>
            /// Outer class reference constructor;
            /// <br />
            /// Конструктор для передачи ссылки на внешний класс;
            /// </summary>
            public ServiceWindowViewModelHandler(ServiceWindowViewModel ViewModelReference)
            {
                _ViewModelRef = ViewModelReference;

                ViewModelRef.Service.SendServiceOutput += OnServiceOutput;
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
