using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.networking.svrPackets
{
    public class RawPacket : ServerPacket
    {
        public byte[] Content { get; set; }

        PacketID id;
        public PacketID PktID { set { id = value; } }
        public override PacketID ID { get { return id; } }
        public override Packet CreateInstance() { return new RawPacket(); }

        protected override void Read(Client client, NReader rdr)
        {
            //
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Content);
        }
    }
}
