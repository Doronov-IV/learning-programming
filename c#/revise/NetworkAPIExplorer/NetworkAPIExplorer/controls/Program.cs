using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkAPIExplorer.Controls
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Namespaces:
            //
            // using System.Net; (default)
            // using System.Net.Sockets;
            //


            #region BASICS


            // 1. Ip Address (useful);
            var tempIpAddressValue = IPAddress.Parse("10.61.140.35"); // Parses 'string' to 'byte[]';

            // 2. Host Entry (rare);
            IPHostEntry hostEntry = new IPHostEntry(); // The name of the exact computer in the network;

            // 3. Dns. Can provide address by hostname and vice-versa (rare);
            var hostAddress = Dns.GetHostEntry(IPAddress.Parse("10.61.140.35")); // returns "s-dev-2.[ROSCOMNADZOR].local";


            #endregion BASICS




            #region WEBCLIENT


            // 4. WebClient (useful);
            // With it, we can download files and do other things;

            // Here I download a file from the Internet;
            WebClient webClient = new();

            string localFileName = "synonym-dictionary-example.pdf"; // I want to re-name the file upon download;

            webClient.DownloadFile
                (
                // link to the file, NOT THE PAGE THAT CONTAINS IT;
                address: @"https://synonymonline.ru/download/o.d.ushakova-sinonimy_i_antonimy.pdf",
                // on download, the file will be
                fileName: localFileName
                );

            FileInfo info = new(localFileName);

            var path = info.FullName; // By default, downloads to bin/debug ... etc;


            #endregion WEBCLIENT




            #region SOCKETS


            // We want to use this exact overload;
            Socket serverSocket = new Socket(
                AddressFamily.InterNetwork,  // our local network;
                SocketType.Stream,           // 'Stream' - TCP socket, 'Dgram' - UDP;
                ProtocolType.IP);            // IP protocol (we have only used ip and udp);


            // Just a pair of address-port values. We use it for connection;
            IPEndPoint ipEndPoint = new IPEndPoint(address: IPAddress.Parse("10.61.140.35"), port: 8000);


            // With socket we can make connection with other devices to push some data then;
            // Target's device must await for (listen to) our connection as well;
            try
            {
                serverSocket.Bind(ipEndPoint);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception. Message: {ex.Message}");
            }


            /*
             
                Server alg:

            1. Create socket;

            2. Bind local endpoint for listenning;

            3. Start listenning;
                                    
            4. Accept client (go to #3); 

            5. Recieve/send data;

            6. Close the socket.


                Client one:

            1. Create socket;

            2. Connect to server; (literally via socketName.Connect())

            3. Send/recieve data;

            4. Close socket.
             
             */

            #endregion SOCKETS


            


        }
    }
}