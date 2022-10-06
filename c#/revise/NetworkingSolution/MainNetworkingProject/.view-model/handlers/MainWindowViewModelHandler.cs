namespace MainNetworkingProject.ViewModel
{
    public partial class MainWindowViewModel
    {
        public class MainWindowViewModelHandler : INotifyPropertyChanged
        {


            #region HANDLERS





            #endregion HANDLERS








            #region CONSTRUCTION




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




            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по умолчанию;
            /// </summary>
            public MainWindowViewModelHandler()
            {

            }



            #endregion CONSTRUCTION


        }
    }
}
