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
                closeWindow.Hide();
            });
        }



        public static void FromChatToLogin(string userLogin)
        {
            

            Window closeWindow = null;
            Window showWindow = null;
            Application.Current.Dispatcher.Invoke(() =>
            {

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

                showWindow.Show();
                closeWindow.Hide();
            });

            

            //ClientLoginWindow window = new ClientLoginWindow(userLogin);
            //window.Show();
            //Application.Current.MainWindow.Close();


        }


    }
}
