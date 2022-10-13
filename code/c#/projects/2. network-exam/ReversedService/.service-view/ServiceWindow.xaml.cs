using ReversedService.ViewModel;
using ReversedService.ViewModel.ServiceWindow;

namespace ReversedService.service_view
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
