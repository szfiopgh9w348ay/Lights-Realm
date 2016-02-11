using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using wServer.networking.svrPackets;

namespace wServer.networking
{
    public abstract class Packet
    {
        public static Dictionary<PacketID, Packet> Packets = new Dictionary<PacketID, Packet>();

        static Packet()
        {
            foreach (var i in typeof(Packet).Assembly.GetTypes())
                if (typeof(Packet).IsAssignableFrom(i) && !i.IsAbstract)
                {
                    Packet pkt = (Packet)Activator.CreateInstance(i);
                    if (!(pkt is ServerPacket))
                        Packets.Add(pkt.ID, pkt);
                }
        }

        public abstract PacketID ID { get; }

        public abstract Packet CreateInstance();

        public abstract byte[] Crypt(Client client, byte[] dat, int len);

        public void Read(Client client, byte[] body, int len)
        {
            Read(client, new NReader(new MemoryStream(Crypt(client, body, len))));
        }

        public byte[] Write(Client client)
        {
            MemoryStream s = new MemoryStream();
            this.Write(client, new NWriter(s));

            byte[] content = s.ToArray();
            byte[] ret = new byte[5 + content.Length];
            content = this.Crypt(client, content, content.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(ret.Length)), 0, ret, 0, 4);
            ret[4] = (byte)this.ID;
            Buffer.BlockCopy(content, 0, ret, 5, content.Length);
            return ret;
        }

        protected abstract void Read(Client client, NReader rdr);

        protected abstract void Write(Client client, NWriter wtr);

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder("{");
            var arr = GetType().GetProperties();
            for (var i = 0; i < arr.Length; i++)
            {
                if (i != 0) ret.Append(", ");
                ret.AppendFormat("{0}: {1}", arr[i].Name, arr[i].GetValue(this, null));
            }
            ret.Append("}");
            return ret.ToString();
        }
    }

    public class NopPacket : Packet
    {
        public override PacketID ID { get { return PacketID.UpdateAck; } }

        public override Packet CreateInstance()
        {
            return new NopPacket();
        }

        public override byte[] Crypt(Client client, byte[] dat, int len)
        {
            return dat;
        }

        protected override void Read(Client client, NReader rdr)
        {
        }

        protected override void Write(Client client, NWriter wtr)
        {
        }
    }
}
