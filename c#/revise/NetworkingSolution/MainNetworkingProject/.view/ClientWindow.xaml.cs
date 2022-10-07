using MainNetworkingProject.ViewModel.ClientWindow;


namespace MainNetworkingProject.View
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
