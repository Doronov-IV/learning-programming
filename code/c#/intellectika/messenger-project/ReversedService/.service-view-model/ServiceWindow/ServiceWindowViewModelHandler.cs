using System.Threading;

namespace ReversedService.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel
    {


        #region CONTROL HANDLERS


        /// <summary>
        /// Handle checkbox 'On' click;
        /// <br />
        /// Обработать нажатие "On" чекбокса;
        /// </summary>
        public async void OnRunClick()
        {
            CustomTerminalManager.AddMessage("Service on.");
            ProcessingStatus.ToggleCompletion();
            var task1 = Service.ListenAuthorizerAsync();
            var task2 = Service.ListenClientsAsync();
            await Task.WhenAll(task1, task2);
        }


        /// <summary>
        /// Handle checkbox 'Off' click;
        /// <br />
        /// Обработать нажатие "Off" чекбокса;
        /// </summary>
        public void OnShutdownClick()
        {
            CustomTerminalManager.AddMessage("Service off.");
            Service.BroadcastShutdown();
            Service.Stop();
            cancellationTokenSource.Cancel();
            ProcessingStatus.ToggleProcessing();
        }


        #endregion CONTROL HANDLERS
    }
}
