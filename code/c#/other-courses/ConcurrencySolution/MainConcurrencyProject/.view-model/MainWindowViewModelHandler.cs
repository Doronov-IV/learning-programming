using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Calculator.PiNumber;
using MainConcurrencyProject.Model.Divisors;
using System;
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
            await CalculateDivisorsAsync();
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



        /// <summary>
        /// Handle progressbar value increased event.
        /// <br />
        /// Обработать событие увеличения значения progressbar'а.
        /// </summary>
        private void OnProgressBarValueIncreased(long value)
        {
            ProgressValue += value;
        }



        #endregion HANDLERS





        #region CALCULATOR TASKS


        private async Task CalculateDivisorsAsync()
        {
            ProcessingStatus.ToggleCompletion();
            Keyboard.ClearFocus();
            (long divisorsNumber, long resultNumber, double timeElapsed) tuple = (0, 0, 0);
            AsynchronousCalculator calculator = new(currentAmountOfThreads: _threadCount);
            calculator.PassProgressbarValueIncrement += OnProgressBarValueIncreased;
            ProgressBarMaximum = DivisorsCalculator.DefaultValueSet.CielingNumber;
            ProgressValue = 0;
            await Task.Run(() => { tuple = calculator.CalculateDivisors(); });
            ProgressValue = ProgressBarMaximum;

            ResultNumber = tuple.resultNumber.ToString() + "  " + tuple.divisorsNumber.ToString();
            elapsedTime = tuple.timeElapsed.ToString();

            string numberWithDots = String.Format("{0:#,0}", tuple.resultNumber);
            string outputString = $"The number is \"{numberWithDots}\", with total devisors number {tuple.divisorsNumber}, time elapsed {tuple.timeElapsed} ms.";

            ResultNumber = tuple.resultNumber.ToString() + "  " + tuple.divisorsNumber.ToString();
            elapsedTime = tuple.timeElapsed.ToString();

            SendOutput(outputString);
            ProcessingStatus.ToggleCompletion();
        }



        private async Task CalculateMonteCarloAsync()
        {
            ProcessingStatus.ToggleCompletion();
            Keyboard.ClearFocus();

            (double resultNumber, double timeElapsed) tuple = new();

            AsynchronousCalculator calculator = new(currentAmountOfThreads: _threadCount);
            calculator.PassProgressbarValueIncrement += OnProgressBarValueIncreased;
            ProgressBarMaximum = PiNumberCalculator.DefaultValueSet.AmountOfPoints;
            ProgressValue = 0;

            await Task.Run(() => { tuple = calculator.CalculatePiNumber(); });
            ProgressValue = ProgressBarMaximum;

            string outputString = $"The number is \"{tuple.resultNumber}\", time elapsed {tuple.timeElapsed} ms.";

            ResultNumber = tuple.resultNumber.ToString();
            elapsedTime = tuple.timeElapsed.ToString();

            SendOutput(outputString);
            ProcessingStatus.ToggleCompletion();

        }


        #endregion CALCULATOR TASKS





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
