using System.Threading;

namespace ReversedService.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel
    {


        #region CONTROLS HANDLERS


        /// <summary>
        /// Handle checkbox 'On' click;
        /// <br />
        /// Обработать нажатие "On" чекбокса;
        /// </summary>
        public async void OnRunClick()
        {
            CustomTerminalManager.AddMessage("Service on.");
            ProcessingStatus.ToggleCompletion();
            Task.Run(() => Service.RunAuthorizerHeed());
            await Task.Run(() => Service.RunClientHeed());
        }


        /// <summary>
        /// Handle checkbox 'Off' click;
        /// <br />
        /// Обработать нажатие "Off" чекбокса;
        /// </summary>
        public void OnShutdownClick()
        {
            CustomTerminalManager.AddMessage("Service off.");
            Service.Stop();
            cancellationTokenSource.Cancel();
            ProcessingStatus.ToggleProcessing();
        }


        #endregion CONTROLS HANDLERS
    }
}
