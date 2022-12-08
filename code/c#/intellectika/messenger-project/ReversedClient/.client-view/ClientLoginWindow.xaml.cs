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


        public ClientLoginWindow(string userLogin) : this()
        {
            var VMRef = DataContext as ClientLoginWindowViewModel;

            VMRef.UserDTOdata.Login = userLogin;
        }
    }
}
