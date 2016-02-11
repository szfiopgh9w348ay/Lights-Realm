﻿namespace wServer.networking.svrPackets
{
    public class CreateResultPacket : ServerPacket
    {
        public int ObjectID { get; set; }
        public int CharacterID { get; set; }

        public override PacketID ID { get { return PacketID.Create_Success; } }

        public override Packet CreateInstance()
        {
            return new CreateResultPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            ObjectID = rdr.ReadInt32();
            CharacterID = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(ObjectID);
            wtr.Write(CharacterID);
        }
    }
}
