using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace MultithreadingExam.misc
{
    public class CodeNoteSpace
    {

        /*
         
                 OSI Levels:
        
        1) Физический;
        2) Канальный;
        3) Сетевой;
        4) Транспортный;
        5) Сеансовый (Сессионный, 'Session layer');
        6) Представления данных ('Presentation layer');
        7) Прикладной уровень ('Application layer').
        
        
     =====================================================
         
         */



        // That thing is just sending 'Hello!' from one PC to another (at best) using sockets;
        //
        // IMPORTANT[!]: make ONLY the third one homework task since first two are obsolete at this point;
        //




        /// <summary>
        /// First batch;
        /// </summary>
        

        private static void ErzatzMainFirst(string[] args)
        {
            // <creating server>
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8888);
            Socket socket = new Socket(addressFamily: AddressFamily.InterNetwork, socketType: SocketType.Stream, protocolType: ProtocolType.IP);
            socket.Bind(iPEndPoint);
            socket.Listen(int.MaxValue);
            //socket.Accept();

            // new;
            Socket client = socket.Accept();
            // </creating server>

            byte[] data = new byte[256];
            // to read data properly;
            StringBuilder strBuilder = new StringBuilder();
            int bytes = 0;

            // while there is a client listening;
            try
            {
                do
                {
                    // recieving data;
                    // p.s.: windows can do just 'client.Recieve(data)' but other systems, including most linuxes, cannot;
                    // this variable somehow keeps do-while loop from disconnecting with 'client.Available > 0' condition which will happen randomly; 
                    bytes = client.Receive(data);

                    // data is binary so we need to translate it;
                    strBuilder.Append(Encoding.UTF8.GetString(data, index: 0, count: bytes));

                } while (client.Available > 0);
                // if you are running a console application;
                // Console.WriteLine("Message is " + strBuilder.ToString());
            }
            catch (Exception exx)
            {
                throw;
            }
        }

        private static void CreateClient()
        {
            // <creating and connecting client>
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Connect(iPEndPoint);
            // </creating and connecting client>

            // sending data to server;
            byte[] data = Encoding.UTF8.GetBytes("Hello!");
            socket.Send(data);

            // if you are running a console application;
            // Console.ReadKey();


            #region RETREIVED COPYPASTE


            // copy-pasted part;
            // when we launched it, Evgeny connected to Alexander and got the message;
            byte[] data2 = new byte[256];
            StringBuilder strBuilder = new StringBuilder();
            int bytes = 0;
            try
            {
                do
                {
                    bytes = socket.Receive(data2);
                    strBuilder.Append(Encoding.UTF8.GetString(data2, index: 0, count: bytes));

                } while (socket.Available > 0);
            }
            catch (Exception exx)
            {
                throw;
            }


            #endregion RETREIVED COPYPASTE

        }



        /// <summary>
        /// Second batch (client);
        /// </summary>


        private static Socket server;
        private static IPEndPoint iPEndPoint;

        private static void ErzatzMainSecond(string[] args)
        {
            // <creating and connecting client>
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Connect(iPEndPoint);
            // </creating and connecting client>


            Thread recievingThread = new Thread(Recieve);
            recievingThread.Start();
            // but it will end too soon;

            while (true)
            {
                // if you're in console once again;
                // the warning is redundant since teacher says it'll work fine regardless;
                byte[] send = Encoding.UTF8.GetBytes(Console.ReadLine());
            }

        }


        private static void Recieve()
        {
            while (true)
            {
                StringBuilder stringBuilder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];

                /*
                do
                {

                } while (server.Available > 0);
                */
                // It can be more simple:   (not sure it works crossplatform)
                stringBuilder.Append(Encoding.UTF8.GetString(data));
                // Console.WriteLine(stringBuilder.ToString());
            }
        }



        /// <summary>
        /// Third batch (server);
        /// </summary>


        //using System.Collection.Generic;


        private static IPEndPoint iPEnd = new IPEndPoint(IPAddress.Any, 8888);

        private static Socket serverB3 = new Socket(AddressFamily.InterNetwork, protocolType: ProtocolType.IP, socketType: SocketType.Stream);

        private static Socket client;

        private static List<Socket> clients = new List<Socket>();


        private static void ErzatzMainThird(string[] args)
        {
            server.Bind(iPEnd);
            server.Listen(int.MaxValue);

            while (true)
            {
                client = serverB3.Accept();
                clients.Add(client);

                // new;

                //
                // Thread clientThread = new Thread(() => Chatting)
                // clientThread.Start();
                //
            }

        }


        // user sends message to server and server sends it back to all users;
        private static void SendToAll(string message)
        {
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(message);

                clients.ForEach(e => e.Send(data));
            }
            catch (Exception ex)
            {
                // Console.WriteLine("User Disconnected!");
            }

            // I lost it :/
        }

        private static void Chatting()
        {
            while (true)
            {
                System.Text.StringBuilder stringBuilder = new StringBuilder();
                try
                {
                    byte[] data = new byte[256];
                    do
                    {

                        // 
                        //
                        // MISSING LINE ARRAY;
                        //
                        //


                    } while (true);
                }
                catch (Exception ex)
                {

                    // Console.WriteLine("Disconnected!");
                }
            }
        }

    }
}
