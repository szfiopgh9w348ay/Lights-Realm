namespace wServer.networking.svrPackets
{
    public class TradeRequestedPacket : ServerPacket
    {
        public string Name { get; set; }

        public override PacketID ID { get { return PacketID.TradeRequested; } }

        public override Packet CreateInstance()
        {
            return new TradeRequestedPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Name = rdr.ReadUTF();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.WriteUTF(Name);
        }
    }
}
