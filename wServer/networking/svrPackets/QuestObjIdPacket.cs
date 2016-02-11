﻿namespace wServer.networking.svrPackets
{
    public class QuestObjIdPacket : ServerPacket
    {
        public int ObjectID { get; set; }

        public override PacketID ID { get { return PacketID.QuestObjId; } }

        public override Packet CreateInstance()
        {
            return new QuestObjIdPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            ObjectID = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(ObjectID);
        }
    }
}
