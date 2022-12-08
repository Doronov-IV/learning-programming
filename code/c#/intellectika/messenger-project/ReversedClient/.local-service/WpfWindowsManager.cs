using ReversedClient.client_view;
using Net.Transmition;
using NetworkingAuxiliaryLibrary.Objects.Entities;

namespace ReversedClient.LocalService
{
    public static class WpfWindowsManager
    {


        public static void FromLoginToChat(User fullUserData, ClientTransmitter serviceTransmitter)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ReversedClientWindow window = new ReversedClientWindow(fullUserData, serviceTransmitter);

                Window closeWindow = null;
                Window showWindow = null;

                foreach (Window win in Application.Current.Windows)
                {
                    if (win is ReversedClientWindow)
                    {
                        showWindow = win;
                    }
                    else if (win is ClientLoginWindow)
                    {
                        closeWindow = win;
                    }
                }

                Application.Current.MainWindow = showWindow;
                showWindow.Show();
                closeWindow.Close();
            });
        }



        public static void FromChatToLogin(string userLogin)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClientLoginWindow window = new(userLogin);

                Window closeWindow = null;
                Window showWindow = null;

                foreach (Window win in Application.Current.Windows)
                {
                    if (win is ReversedClientWindow)
                    {
                        closeWindow = win;
                    }
                    else if (win is ClientLoginWindow)
                    {
                        showWindow = win;
                    }
                }

                Application.Current.MainWindow = showWindow;
                showWindow.Show();
                closeWindow.Close();
            });
        }


    }
}
