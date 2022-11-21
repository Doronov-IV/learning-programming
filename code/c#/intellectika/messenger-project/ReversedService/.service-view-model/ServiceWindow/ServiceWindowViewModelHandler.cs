using System.Threading;

namespace ReversedService.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel
    {


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
            ServiceLog.Add(sServiceOutput);
        }


        #endregion PENDING OUTPUT





        #region CONTROLS HANDLERS


        /// <summary>
        /// Handle 'Run' click;
        /// <br />
        /// Обработать нажатие "Run";
        /// </summary>
        public async void OnRunClick()
        {
            OnServiceOutput("Service on.");
            await Task.Run(() => Service.Run());
            ProcessingStatus.ToggleCompletion();
        }


        public void OnShutdownClick()
        {
            OnServiceOutput("Service off.");
            Service.Stop();
            ProcessingStatus.ToggleProcessing();
        }


        #endregion CONTROLS HANDLERS
    }
}
