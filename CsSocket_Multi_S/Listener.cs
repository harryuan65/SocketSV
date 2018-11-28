using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace CsSocket_Multi_S
{
    class Listener
    {
        Socket s;
        public bool Listening
        {
            get;
            private set;
        }
        public int Port
        {
            get;
            private set;
        }
        public Listener(int port)
        {
            Port = port;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start()
        {
            if (Listening)
                return;
            s.Bind(new IPEndPoint(0, Port));//0 is any(automatically detect)
            s.Listen(0);
            s.BeginAccept(callback,null);//spins off a new thread
            Listening = true;

            Console.WriteLine("Hey my friends! Over Here ,suka!");

        }
        public void Stop()
        {
            if (!Listening)
                return;
            s.Close();
            s.Dispose();
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        void callback(IAsyncResult ar)
        {
            try
            {
                Socket s = this.s.EndAccept(ar);//Ends the thread?

                SocketAccepted?.Invoke(s);//若事件沒被觸發就以s觸發
                this.s.BeginAccept(callback, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;



    }
}
