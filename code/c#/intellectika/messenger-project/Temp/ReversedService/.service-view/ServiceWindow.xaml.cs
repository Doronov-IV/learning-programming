using ReversedService.ViewModel;
using ReversedService.ViewModel.ServiceWindow;

using Simple.Wpf.Terminal;
using ToggleSwitch;

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


        public void OnWindowClosing(object? sender, CancelEventArgs args)
        {
            var vmRef = DataContext as ServiceWindowViewModel;
            vmRef.Service.BroadcastShutdown();

            Application.Current.Shutdown();
        }
    }
}
