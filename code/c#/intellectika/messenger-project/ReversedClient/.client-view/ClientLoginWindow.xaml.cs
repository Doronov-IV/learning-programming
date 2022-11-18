using ReversedClient.ViewModel;

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

            DataContext = new ReversedClientWindowViewModel();
        }
    }
}
