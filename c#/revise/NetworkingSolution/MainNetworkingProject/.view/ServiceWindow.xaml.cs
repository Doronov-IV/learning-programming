using MainNetworkingProject.ViewModel.ServiceWindow;

namespace MainNetworkingProject.View
{
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();

            DataContext = new ServiceWindowViewModel();
        }
    }
}
