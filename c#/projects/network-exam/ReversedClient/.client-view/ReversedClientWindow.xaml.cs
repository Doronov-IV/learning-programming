using ReversedClient.ViewModel;

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

            DataContext = new ReversedClientWindowViewModel();
        }
    }
}
