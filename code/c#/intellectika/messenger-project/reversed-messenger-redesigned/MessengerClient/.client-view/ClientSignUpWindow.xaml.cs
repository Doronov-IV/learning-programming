using System;
using System.Collections.Generic;
using Net.Transmition;
using NetworkingAuxiliaryLibrary.Objects.Common;
using MessengerClient.ViewModel.ClientSignUpWindow;
using MessengerClient.ViewModel.ClientStartupWindow;

namespace MessengerClient.View
{
    /// <summary>
    /// Interaction logic for ClientSignUpWindow.xaml
    /// </summary>
    public partial class ClientSignUpWindow : Window
    {
        public ClientSignUpWindow()
        {
            InitializeComponent();

            DataContext = new ClientSignUpWindowViewModel();

            Name = nameof(ClientSignUpWindow);
        }


        public ClientSignUpWindow(UserClientTechnicalDTO user, ClientTransmitter transmitter) : this()
        {
            DataContext = new ClientSignUpWindowViewModel(user, transmitter);
        }


        public void OnClosing(object? sender, EventArgs args)
        {
            Application.Current.Shutdown();
        }
    }
}
