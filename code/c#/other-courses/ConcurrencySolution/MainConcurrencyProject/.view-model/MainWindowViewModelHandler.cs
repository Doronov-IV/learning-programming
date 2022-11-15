using MainConcurrencyProject.Model.Calculator;
using System.Diagnostics;
using System.Windows.Input;

namespace MainConcurrencyProject.ViewModel
{
    /// <summary>
    /// Tier 1 - handlers, tier 2 - various secondary methods, tier 3 - auxiliary methods, tier 4 - dispatches, tier 5 - generic i/o.
    /// <br />
    /// Tier 1 - обработчики, tier 2 - различные второстепенные методы, tier 3 - вспомогательные методы, tier 4 - диспатчи и tier 5 - генерализованный ввод/вывод.
    /// </summary>
    public partial class MainWindowViewModel
    {

        // handle controls events

        #region HANDLERS



        /// <summary>
        /// Handle 'Do Action' button click.
        /// <br />
        /// Обработать клик по кнопке "Do Action".
        /// </summary>
        private async void OnDoActionButtonClickAsync()
        {
            ProcessingStatus.ToggleCompletion();
            Keyboard.ClearFocus();
            (long divisorsNumber , long resultNumber, long timeElapsed) tuple = (0, 0, 0); 
            AsynchronousCalculator calculator = new(currentAmountOfThreads: _threadCount);
            await Task.Run(() => { tuple = calculator.CalculateDivisors(); });

            ResultNumber = tuple.resultNumber.ToString() + "  " + tuple.divisorsNumber.ToString();
            elapsedTime = tuple.timeElapsed.ToString();

            string numberWithDots = String.Format("{0:#,0}", tuple.resultNumber);
            string outputString = $"The number is \"{numberWithDots}\", with total devisors number {tuple.divisorsNumber}, time elapsed {tuple.timeElapsed} ms.";

            ResultNumber = tuple.resultNumber.ToString() + "  " + tuple.divisorsNumber.ToString();
            elapsedTime = tuple.timeElapsed.ToString();

            SendOutput(outputString);
            ProcessingStatus.ToggleCompletion();
        }



        /// <summary>
        /// Handle 'space' key press event.
        /// <br />
        /// Обработать событие нажатия на кнопку "пробел".
        /// </summary>
        public void OnPauseKeyButtonPressed()
        {
            if (ProcessingStatus.IsRunning)
            {
                AsynchronousCalculator.maunalResetHandler.Reset();
                ProcessingStatus.ToggleProcessing();
                PauseContinueActionName = "Continue";
            }
            else
            {
                AsynchronousCalculator.maunalResetHandler.Set();
                ProcessingStatus.ToggleProcessing();
                PauseContinueActionName = "Pause";
            }
        }



        #endregion HANDLERS





        #region IO


        /// <summary>
        /// Send output to the greenboard.
        /// <br />
        /// Отправить данные в листбокс.
        /// </summary>
        /// <param name="messsage">
        /// The text message to be sent.
        /// <br />
        /// Текстовое сообщение для отправления.
        /// </param>
        private void SendOutput(string message)
        {
            Application.Current.Dispatcher.Invoke(() => { OutputCollection.Add(message); } );
        }


        #endregion IO


    }
}
