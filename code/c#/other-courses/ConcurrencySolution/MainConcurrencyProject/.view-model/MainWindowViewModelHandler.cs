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

        #region HANDLERS - Tier 1



        /// <summary>
        /// Handle 'Do Action' button click.
        /// <br />
        /// Обработать клик по кнопке "Do Action".
        /// </summary>
        private void OnDoActionButtonClick()
        {
            RunCounter();
        }



        #endregion HANDLERS - Tier 1





        // handle topics events

        #region SECONDARY - Tier 2



        /// <summary>
        /// Run Parametrized thread demo.
        /// <br />
        /// Запустить демо Потоки с параметрами.
        /// </summary>
        private void RunHellos()
        {
            Thread myThread1 = new Thread(new ParameterizedThreadStart(Print));
            Thread myThread2 = new Thread(Print);
            Thread myThread3 = new Thread(message => MessageBox.Show(message as string, "Output", MessageBoxButton.OK, MessageBoxImage.Information));

            myThread1.Start("Hello");
            myThread2.Start("Привет");
            myThread3.Start("Salut");
        }



        /// <summary>
        /// Run shared resources demo.
        /// <br />
        /// Запустить демку по разделяемым ресурсам.
        /// </summary>
        private void RunCounter()
        {
            Thread thread;

            // to lock/unlock, switch methods in tier 3 region.
            for (int i = 0, iSize = 5; i < iSize; ++i)
            {
                thread = new(Print);
                thread.Name = $"Thread №{i}";
                thread.Start(1);
                //thread.Join();
            }

            Thread.Sleep(2000);
            ShowOutput(_largeMessage);
        }



        #endregion SECONDARY - Tier 2





        // handle excercises events

        #region AUXILIARY - Tier 3 



        /// <summary>
        /// Call messagebox to display output message.
        /// <br />
        /// Вызвать messagebox, чтобы отобразить сообщение для вывода.
        /// </summary>
        private void Print(object? message)
        {
            if (message is not null)
            {
                if (message is string)
                {
                    ShowOutput(message.ToString());
                }
                else if (message is int)
                {
                    DoSharedExampleWithSemaphore((int)message);
                }
            }
        }



        #endregion AUXILIARY - Tier 3 





        // dispatched functions for different api

        #region ELEMENTARY - Tier 4



        /// <summary>
        /// Increment a number in a loop W/O locking.
        /// <br />
        /// Проинкрементировать число в цикле БЕЗ лока.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedNumberExampleWithoutLocker(int number)
        {
            DoIncrement(number);
        }



        /// <summary>
        /// Increment a number in a loop W/ locking.
        /// <br />
        /// Проинкрементировать число в цикле С локом.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithLocker(int number)
        {
            lock (_locker)
            {
                DoIncrement(number);
            }
        }



        /// <summary>
        /// Increment a number in a loop W/ auto reset event.
        /// <br />
        /// Проинкрементировать число в цикле С авто ресет ивентом.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithAutoResetEvent(int number)
        {
            _autoResetHandler.WaitOne();
            DoIncrement(number);
            _autoResetHandler.Set();
        }



        /// <summary>
        /// Increment a number in a loop W/ mutex.
        /// <br />
        /// Проинкрементировать число в цикле С мьютексом.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithMutex(int number)
        {
            _mutex.WaitOne();
            DoIncrement(number);
            _mutex.ReleaseMutex();
        }



        /// <summary>
        /// Increment a number in a loop W/ semaphore.
        /// <br />
        /// Проинкрементировать число в цикле С семафором.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithSemaphore(int number)
        {
            _semaphore.WaitOne();
            DoIncrement(number);
            _semaphore.Release();
        }



        #endregion ELEMENTARY - Tier 4





        // actual generic i/o

        #region ELEMENTARY - Tier 5




        /// <summary>
        /// Increment a number in a loop.
        /// <br />
        /// Проинкрементировать число в цикле.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoIncrement(int number)
        {
            for (int i = 0, iSize = 6; i < iSize; ++i)
            {
                _largeMessage += Thread.CurrentThread.Name + ": " + number.ToString() + "\n";
                number++;
                Thread.Sleep(10);
            }
        }


        /// <summary>
        /// Show messagebox output message.
        /// <br />
        /// Отобразить сообщение в messagebox'е.
        /// </summary>
        private void ShowOutput(string message)
        {
            MessageBox.Show(message, "Output", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        #endregion ELEMENTARY - Tier 5



    }
}
