using ReversedClient.ViewModel.ClientChatWindow;

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

            ClientLoginWindow? clientLoginWindow = Application.Current.MainWindow as ClientLoginWindow;

            ReversedClientWindowViewModel? context = clientLoginWindow?.DataContext as ReversedClientWindowViewModel;

            DataContext = context;
        }
    }
}
