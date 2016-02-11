namespace wServer.networking.svrPackets
{
    public class GotoPacket : ServerPacket
    {
        public int ObjectId { get; set; }
        public Position Position { get; set; }

        public override PacketID ID { get { return PacketID.Goto; } }

        public override Packet CreateInstance()
        {
            return new GotoPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Position = Position.Read(rdr);
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(ObjectId);
            Position.Write(wtr);
        }
    }
}
