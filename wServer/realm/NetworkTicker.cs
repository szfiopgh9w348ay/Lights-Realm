using log4net;
using System;
using System.Collections.Concurrent;
using System.Threading;
using wServer.networking;

namespace wServer.realm
{
    public class NetworkTicker //Sync network processing
    {
        private ILog log = LogManager.GetLogger(typeof(NetworkTicker));

        //public void AddPendingAction(Client client, Action<RealmTime> callback)
        public void AddPendingPacket(Client client, Packet packet)
        {
            pendings.Enqueue(new Tuple<Client, Packet>(client, packet));//Action<RealmTime>>(client, callback));
            handle.Set();
        }

        private AutoResetEvent handle = new AutoResetEvent(false);

        //static ConcurrentQueue<Tuple<Client, Action<RealmTime>>> pendings = new ConcurrentQueue<Tuple<Client, Action<RealmTime>>>();
        private static ConcurrentQueue<Tuple<Client, Packet>> pendings = new ConcurrentQueue<Tuple<Client, Packet>>();

        public void TickLoop()
        {
            do
            {
                foreach (var i in RealmManager.Clients)
                    if (i.Value.Stage == ProtocalStage.Disconnected)
                    {
                        Client client;
                        RealmManager.Clients.TryRemove(i.Key, out client);
                    }

                handle.WaitOne();

                Tuple<Client, Packet> work;//Action<RealmTime>> work;
                while (pendings.TryDequeue(out work))
                {
                    if (work.Item1.Stage == ProtocalStage.Disconnected)
                    {
                        Client client;
                        RealmManager.Clients.TryRemove(work.Item1.Account.AccountId, out client);
                        continue;
                    }
                    try
                    {
                        work.Item1.ProcessPacket(work.Item2);
                        //work.Item2(LogicTicker.CurrentTime);
                    }
                    catch { }
                }
            } while (true);
        }
    }
}
