namespace wServer.networking.svrPackets
{
    public class NameResultPacket : ServerPacket
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public override PacketID ID { get { return PacketID.NameResult; } }

        public override Packet CreateInstance()
        {
            return new NameResultPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Success = rdr.ReadBoolean();
            Message = rdr.ReadUTF();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Success);
            wtr.WriteUTF(Message);
        }
    }
}
