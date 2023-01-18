using Net.Transmition;
using NetworkingAuxiliaryLibrary.Objects.Common;
using ReversedClient.ViewModel.ClientStartupWindow;

namespace ReversedClient.client_view
{
    /// <summary>
    /// Interaction logic for ClientLoginWindow.xaml
    /// </summary>
    public partial class ClientLoginWindow : Window
    {

        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ClientLoginWindow()
        {
            InitializeComponent();

            DataContext = new ClientLoginWindowViewModel();

            Name = nameof(ClientLoginWindow);
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public ClientLoginWindow(UserClientTechnicalDTO userData, ClientTransmitter transmitter) : this()
        {
            if (userData is not null && transmitter is not null)
                DataContext = new ClientLoginWindowViewModel(new(userData.Login, "", ""), transmitter);
            else DataContext = new ClientLoginWindowViewModel();
        }



        /// <summary>
        /// Handle window closing event.
        /// <br />
        /// By default, wpf windows launch in a new separate process. When the window is closed, this process goes background but does not end. I made this handler to fix this issue.
        /// <br />
        /// <br />
        /// Обработать событие закрытия окна.
        /// <br />
        /// По умолчанию, окна wpf запускаются в отдельном новом процессе. При закрытии окна, этот процесс становится фоновым, но не закрывается. Я сделал этот обработчик, чтобы исправить данную проблему.
        /// </summary>
        public void OnClosing(object? sender, EventArgs args)
        {
            Application.Current.Shutdown();
        }


    }
}
