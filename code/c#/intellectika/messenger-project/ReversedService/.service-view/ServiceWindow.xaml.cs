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
            Process.GetProcesses().ToList().Find(n => n.ProcessName == "ReversedService")?.Kill();
        }
    }
}
