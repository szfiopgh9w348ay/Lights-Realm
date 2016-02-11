namespace wServer.networking.cliPackets
{
    public class UsePortalPacket : ClientPacket
    {
        public int ObjectId { get; set; }

        public override PacketID ID { get { return PacketID.UsePortal; } }

        public override Packet CreateInstance()
        {
            return new UsePortalPacket();
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
