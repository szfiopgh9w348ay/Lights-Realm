using db;
using log4net;
using log4net.Config;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using wServer.networking;
using wServer.realm;

namespace wServer
{
    internal static class Program
    {
        internal static ILog logger { get; } = LogManager.GetLogger("Server");
        internal static SimpleSettings Settings;
        private static Socket svrSkt;

        private static void HostPolicyServer()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 843);
                listener.Start();
                listener.BeginAcceptTcpClient(ServePolicyFile, listener);
            }
            catch { }
        }

        private static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net_wServer.config"));

            Settings = new SimpleSettings("wServer");

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            svrSkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            svrSkt.Bind(new IPEndPoint(IPAddress.Any, 2050));
            svrSkt.Listen(0xff);
            svrSkt.BeginAccept(Listen, null);
            Console.CancelKeyPress += (sender, e) =>
            {
                logger.Info("Saving Please Wait...");
                svrSkt.Close();
                foreach (var i in RealmManager.Clients.Values.ToArray())
                {
                    i.Save();
                    i.Disconnect();
                }
                logger.Info("Closing...");
                Thread.Sleep(500);
                Environment.Exit(0);
            };

            logger.Info("Listening at port 2050...");

            HostPolicyServer();

            RealmManager.CoreTickLoop();    //Never returns
        }

        private static void ServePolicyFile(IAsyncResult ar)
        {
            TcpClient cli = (ar.AsyncState as TcpListener).EndAcceptTcpClient(ar);
            (ar.AsyncState as TcpListener).BeginAcceptTcpClient(ServePolicyFile, ar.AsyncState);
            try
            {
                var s = cli.GetStream();
                NReader rdr = new NReader(s);
                NWriter wtr = new NWriter(s);
                if (rdr.ReadNullTerminatedString() == "<policy-file-request/>")
                {
                    wtr.WriteNullTerminatedString(@"<cross-domain-policy>
     <allow-access-from domain=""*"" to-ports=""*"" />
</cross-domain-policy>");
                    wtr.Write((byte)'\r');
                    wtr.Write((byte)'\n');
                }
                cli.Close();
            }
            catch { }
        }

        private static void Listen(IAsyncResult ar)
        {
            Socket skt = null;
            try
            {
                skt = svrSkt.EndAccept(ar);
            }
            catch (ObjectDisposedException)
            {
            }
            try
            {
                svrSkt.BeginAccept(Listen, null);
            }
            catch (ObjectDisposedException)
            {
            }
            if (skt != null)
            {
                var psr = new Client(skt);
                psr.BeginProcess();
            }
        }
    }
}
