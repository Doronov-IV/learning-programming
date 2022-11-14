using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MainConcurrencyProject.ViewModel;

namespace MainConcurrencyProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }


        public void OnWindowClosing(object? sender, CancelEventArgs args)
        {
            Process.GetProcesses().ToList().Find(n => n.ProcessName == "MainConcurrencyProject")?.Kill();
        }
    }
}
