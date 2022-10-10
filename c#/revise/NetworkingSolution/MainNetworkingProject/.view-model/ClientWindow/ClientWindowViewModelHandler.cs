using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel.ClientWindow
{
    public partial class ClientWindowViewModel
    {
        public class ClientWindowViewModelHandler : INotifyPropertyChanged
        {


            private ClientWindowViewModel _ViewModelReference;


            public ClientWindowViewModel ViewModelReference
            {
                get { return _ViewModelReference; }
                set
                {
                    _ViewModelReference = value;
                    OnPropertyChanged(nameof(ViewModelReference));
                }
            }




            #region HANDLERS


            public void SendMessage()
            {
                if (ViewModelReference.UserMessage != "" && null != ViewModelReference.UserMessage)
                {
                    ViewModelReference.MainExplorerClient.UserMessage = ViewModelReference.UserMessage;
                    ViewModelReference.MainExplorerClient.SendMessage();
                }
            }




            public void UpdateChatLog()
            {
                ViewModelReference.ChatLog = ViewModelReference.MainExplorerClient.ChatLog;
            }



            public void Connect()
            {
                ViewModelReference.MainExplorerClient.Connect();
                ViewModelReference.MainExplorerClient.Run();
            }


            #endregion HANDLERS



            #region CONSTRUCTION




            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по умолчанию;
            /// </summary>
            public ClientWindowViewModelHandler(ClientWindowViewModel ViewModelReference)
            {
                _ViewModelReference = ViewModelReference;
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
