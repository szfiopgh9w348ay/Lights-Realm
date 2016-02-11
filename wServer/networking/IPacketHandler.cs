using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.networking.cliPackets;
using wServer.realm;

namespace wServer.networking
{
    internal interface IPacketHandler
    {
        PacketID ID { get; }
        void Handle(Client client, ClientPacket packet);
    }
    internal abstract class PacketHandlerBase<T> : IPacketHandler where T : ClientPacket
    {
        protected ILog log;
        private Client client;

        public PacketHandlerBase()
        {
            log = LogManager.GetLogger(GetType());
        }

        public abstract PacketID ID { get; }

        public void Handle(Client client, ClientPacket packet)
        {
            this.client = client;
            HandlePacket(client, (T)packet);
        }

        public RealmManager Manager { get { return client.Manager; } }
        public Client Client { get { return client; } }

        protected abstract void HandlePacket(Client client, T packet);

        protected void SendFailure(string text)
        {
            client.SendPacket(new FailurePacket() { Message = text });
        }
    }

    internal class PacketHandlers
    {
        public static Dictionary<PacketID, IPacketHandler> Handlers = new Dictionary<PacketID, IPacketHandler>();

        static PacketHandlers()
        {
            foreach (Type i in typeof(Packet).Assembly.GetTypes())
            {
                if (typeof(IPacketHandler).IsAssignableFrom(i) &&
                    !i.IsAbstract && !i.IsInterface)
                {
                    IPacketHandler pkt = (IPacketHandler)Activator.CreateInstance(i);
                    Handlers.Add(pkt.ID, pkt);
                }
            }
        }
    }
}
