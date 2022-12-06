﻿using System.Threading;

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
            var task1 = Service.RunAuthorizerHeed();
            var task2 = Service.RunClientHeed();
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


        #endregion CONTROLS HANDLERS
    }
}
