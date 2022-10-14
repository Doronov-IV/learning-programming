using ReversedService.Net.Main;
using System.Threading;

namespace ReversedService.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel
    {
        /// <summary>
        /// A part of a view-model, required to decompose it;
        /// <br />
        /// Часть вью-модели, необходимая для её декомпозирования;
        /// </summary>
        public class ServiceWindowViewModelHandler : INotifyPropertyChanged
        {


            #region PROPERTIES



            /// <summary>
            /// @see public ServiceWindowViewModel ViewModelReference;
            /// </summary>
            private ServiceWindowViewModel _ViewModelReference;

            /// <summary>
            /// An instance of corresponding view-model;
            /// <br />
            /// Экземпляр соответствующей вью-модели;
            /// </summary>
            public ServiceWindowViewModel ViewModelReference
            {
                get { return _ViewModelReference; }
                set
                {
                    _ViewModelReference = value;
                    OnPropertyChanged(nameof(ViewModelReference));
                }
            }



            #endregion PROPERTIES





            #region PENDING OUTPUT


            /// <summary>
            /// Service feedback output handling;
            /// <br />
            /// Обработка сообщений обратной связи сервера;
            /// </summary>
            /// <param name="sServiceOutput">
            /// The text message sent by server;
            /// <br />
            /// Текстовое сообщение, отправленное сервером;
            /// </param>
            public void OnServiceOutput(string sServiceOutput)
            {
                ViewModelReference.ServiceLog.Add(sServiceOutput);       
            }


            #endregion PENDING OUTPUT





            #region CONTROLS HANDLERS


            /// <summary>
            /// Handle 'Run' button click;
            /// <br />
            /// Обработать нажатие по кнопке "Run";
            /// </summary>
            public void OnRunButtonClick()
            {
                Thread runninThread = new Thread(ViewModelReference.Service.Run);
                runninThread.Start();
                ToggleConnectionState();
            }


            /// <summary>
            /// Handle 'Cancel Service' button click;
            /// <br />
            /// Обработать нажатие по кнопке "Cancel Service";
            /// </summary>
            public void OnCancelServiceButtonClick()
            {
                Process.GetProcesses().ToList().Find(n => n.ProcessName == "ReversedService")?.Kill();
            }


            #endregion CONTROLS HANDLERS





            #region LOGIC



            /// <summary>
            /// Toggle connection state from 'true' to 'false' or back;
            /// <br />
            /// Сменить статус подключения с "true" на "false" или обратно;
            /// </summary>
            private void ToggleConnectionState()
            {
                bool bTemp = _ViewModelReference.IsRunning;
                _ViewModelReference.IsRunning = _ViewModelReference.IsNotRunning;
                _ViewModelReference.IsNotRunning = bTemp;
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
                _ViewModelReference = ViewModelReference;

                this.ViewModelReference.Service.SendServiceOutput += OnServiceOutput;
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
