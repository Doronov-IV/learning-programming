using Debug.Net;
using ReversedClient.ViewModel.ClientChatWindow;
using ReversedClient.ViewModel.Misc;

namespace ReversedClient.client_view
{
    /// <summary>
    /// Interaction logic for ReversedClientWindow.xaml
    /// </summary>
    public partial class ReversedClientWindow : Window
    {
        public ReversedClientWindow()
        {
            InitializeComponent();

            DataContext = new ReversedClientWindowViewModel();
        }


        public ReversedClientWindow(UserDTO userTransferObject, ClientTransmitter clientRadio)
        {
            InitializeComponent();

            DataContext = new ReversedClientWindowViewModel(userTransferObject, clientRadio);
        }
    }
}
