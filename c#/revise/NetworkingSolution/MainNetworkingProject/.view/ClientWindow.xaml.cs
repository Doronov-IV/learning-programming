using MainNetworkingProject.ViewModel.ClientWindow;


namespace MainNetworkingProject.view
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();

            DataContext = new ClientWindowViewModel();
        }
    }
}
