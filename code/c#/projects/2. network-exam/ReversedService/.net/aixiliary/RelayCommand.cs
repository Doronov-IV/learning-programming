namespace ReversedService.Net.Auxiliary
{
    public class RelayCommand : ICommand
    {


        #region PROPERTIES


        /// <summary>
        /// Execution event;
        /// <br />
        /// Событие выполнения;
        /// </summary>
        private Action<object> execute;


        /// <summary>
        /// Execution opportunity predicate;
        /// <br />
        /// Предикат возможности выполнения;
        /// </summary>
        private Func<object, bool> canExecute;


        /// <summary>
        /// Can execute changed event handler;
        /// <br />
        /// Хендлер обработки события смены предиката возможности выполнения;
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        #endregion PROPERTIES




        #region API



        /// <summary>
        /// A predicate that can determine whether the commend can execute or not;
        /// <br />
        /// Предикат, который определяет, может ли команда выполниться;
        /// </summary>
        /// <returns>
        /// True if command can execute, otherwise false;
        /// <br />
        /// True если команда может быть выполнена, иначе false;
        /// </returns>
        public bool CanExecute(object? parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }


        /// <summary>
        /// Execute command;
        /// <br />
        /// Выполнить команду;
        /// </summary>
        public void Execute(object? parameter)
        {
            this.execute(parameter);
        }



        #endregion API




        #region CONSTRUCTION


        /// <summary>
        /// Parametrised constructor;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="execute">
        /// The execution action;
        /// <br />
        /// Действие для выполнения;
        /// </param>
        /// <param name="canExecute">
        /// A method that predicts whether the commend can execute at the moment;
        /// <br />
        /// Метод, которы определяет, может ли команда быть выполнена;
        /// </param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        #endregion CONSTRUCTION


    }
}
