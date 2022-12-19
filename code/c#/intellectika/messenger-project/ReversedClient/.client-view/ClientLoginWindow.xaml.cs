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
        public ClientLoginWindow()
        {
            InitializeComponent();

            DataContext = new ClientLoginWindowViewModel();

            Name = nameof(ClientLoginWindow);
        }


        public ClientLoginWindow(UserClientTechnicalDTO userData, ClientTransmitter transmitter) : this()
        {
            DataContext = new ClientLoginWindowViewModel(new(userData.Login, "", ""), transmitter);
        }
    }
}
