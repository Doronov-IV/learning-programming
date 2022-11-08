namespace MainConcurrencyProject.ViewModel
{
    /// <summary>
    /// Tier 1 - handlers, tier 2 - various secondary methods and tier 3 - auxiliary methods.
    /// <br />
    /// Tier 1 - обработчики, tier 2 - различные второстепенные методы, tier 3 - вспомогательные методы.
    /// </summary>
    public partial class MainWindowViewModel
    {



        #region HANDLERS - Tier 1



        /// <summary>
        /// Handle 'Do Action' button click.
        /// <br />
        /// Обработать клик по кнопке "Do Action".
        /// </summary>
        private void OnDoActionButtonClick()
        {
            RunHellos();
        }



        #endregion HANDLERS - Tier 1







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



        #endregion SECONDARY - Tier 2







        #region AUXILIARY - Tier 3 



        /// <summary>
        /// Call messagebox to display output message.
        /// <br />
        /// Вызвать messagebox, чтобы отобразить сообщение для вывода.
        /// </summary>
        void Print(object? message) => MessageBox.Show(message as string, "Output", MessageBoxButton.OK, MessageBoxImage.Information);



        #endregion AUXILIARY - Tier 3 



    }
}
