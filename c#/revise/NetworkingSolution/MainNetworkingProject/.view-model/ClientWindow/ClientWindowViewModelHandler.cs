using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel.ClientWindow
{
    public partial class ClientWindowViewModel
    {
        public class ClientWindowViewModelHandler : INotifyPropertyChanged
        {


            #region HANDLERS


            public void SendMessage()
            {
                ExplorerClient explorerClient = new();

            }


            #endregion HANDLERS



            #region CONSTRUCTION




            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по умолчанию;
            /// </summary>
            public ClientWindowViewModelHandler()
            {

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
