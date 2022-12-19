using NetworkingAuxiliaryLibrary.Objects.Entities;
using ReversedClient.ViewModel.ClientChatWindow;
using ReversedClient.Model;
using System.Windows.Shell;
using Net.Transmition;
using NetworkingAuxiliaryLibrary.Objects.Common;

namespace ReversedClient.client_view
{
    /// <summary>
    /// Interaction logic for ClientMessengerWindow.xaml
    /// </summary>
    public partial class ClientMessengerWindow : Window
    {
        public ClientMessengerWindow()
        {
            InitializeComponent();

            DataContext = new ClientMessengerWindowViewModel();

            SetDefaultName();
        }


        public ClientMessengerWindow(UserServerSideDTO userData, ClientTransmitter clientRadio)
        {
            InitializeComponent();

            DataContext = new ClientMessengerWindowViewModel(userData, clientRadio);

            SetDefaultName();
        }


        public void SetDefaultName()
        {
            Name = "ClientMessengerWindow";
        }


        public void OnClosing(object? sender, CancelEventArgs args)
        {
            Application.Current.Shutdown();
        }
    }
}
