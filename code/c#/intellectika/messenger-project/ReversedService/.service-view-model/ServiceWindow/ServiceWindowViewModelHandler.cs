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
        /// Handle checkbox 'On' click;
        /// <br />
        /// Обработать нажатие "On" чекбокса;
        /// </summary>
        public async void OnRunClick()
        {
            OnServiceOutput("Service on.");
            ProcessingStatus.ToggleCompletion();
            await Task.Run(() => Service.Run());
        }


        /// <summary>
        /// Handle checkbox 'Off' click;
        /// <br />
        /// Обработать нажатие "Off" чекбокса;
        /// </summary>
        public void OnShutdownClick()
        {
            OnServiceOutput("Service off.");
            Service.Stop();
            cancellationTokenSource.Cancel();
            ProcessingStatus.ToggleProcessing();
        }


        #endregion CONTROLS HANDLERS
    }
}
