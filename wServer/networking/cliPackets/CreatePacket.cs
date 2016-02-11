namespace wServer.networking.cliPackets
{
    public class CreatePacket : ClientPacket
    {
        public short ObjectType { get; set; }

        public override PacketID ID { get { return PacketID.Create; } }

        public override Packet CreateInstance()
        {
            return new CreatePacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            ObjectType = rdr.ReadInt16();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(ObjectType);
        }
    }
}
