using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CsSocket_Multi_S
{
    class Program
    {
        static Listener l;
        static List<Socket> sockets;

        static void Main(string[] args)
        {
            l = new Listener(8);//port 8
            l.SocketAccepted += new Listener.SocketAcceptedHandler(L_SocketAccepted);
            l.Start();
            sockets = new List<Socket>();
            Console.Read();
        }
        
       static void L_SocketAccepted(Socket e)
        {
            Console.WriteLine("New Connection:{0} \n{1}\n==========", e.RemoteEndPoint, DateTime.Now);
            sockets.Add(e);
        }
    }
}
