namespace MainNetworkingProject.ViewModel.ClientWindow
{
    public partial class ClientWindowViewModel
    {
        public class ClientWindowViewModelState : INotifyPropertyChanged
        {


            #region PROPERTIES


            private List<string> _ChatLog;

            public List<string> ChatLog
            {
                get { return _ChatLog; }
                set
                {
                    _ChatLog = value;
                    OnPropertyChanged(nameof(ChatLog));
                }
            }



            private string _UserMessage;

            public string UserMessage
            {
                get { return _UserMessage; }
                set
                {
                    UserMessage = value;
                    OnPropertyChanged(nameof(UserMessage));
                }
            }


            #endregion PROPERTIES




            #region CONSTRUCTION




            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по умолчанию;
            /// </summary>
            public ClientWindowViewModelState()
            {
                _ChatLog = new();
            }




            #region Property changed


            /// <summary>
            /// Propery changed event handler;
            /// <br />
            /// Делегат-обработчик события 'property changed';
            /// </summary>
            public event PropertyChangedEventHandler? PropertyChanged;


            /// <summary>
            /// Handler-method of the 'property changed' delegate;
            /// <br />
            /// Метод-обработчик делегата 'property changed';
            /// </summary>
            /// <param name="propName">The name of the property;<br />Имя свойства;</param>
            private void OnPropertyChanged(string propName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }


            #endregion Property changed





            #endregion CONSTRUCTION


        }
    }
}
