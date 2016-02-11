namespace wServer.networking.svrPackets
{
    public class InvResultPacket : ServerPacket
    {
        public int Result { get; set; }

        public override PacketID ID { get { return PacketID.InvResult; } }

        public override Packet CreateInstance()
        {
            return new InvResultPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Result = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Result);
        }
    }
}
