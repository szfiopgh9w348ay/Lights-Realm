namespace wServer.networking.cliPackets
{
    public class BuyPacket : ClientPacket
    {
        public int ObjectId { get; set; }

        public override PacketID ID { get { return PacketID.Buy; } }

        public override Packet CreateInstance()
        {
            return new BuyPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(ObjectId);
        }
    }
}
