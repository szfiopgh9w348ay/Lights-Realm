namespace wServer.networking.cliPackets
{
    public class TeleportPacket : ClientPacket
    {
        public int ObjectId { get; set; }

        public override PacketID ID { get { return PacketID.Teleport; } }

        public override Packet CreateInstance()
        {
            return new TeleportPacket();
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
