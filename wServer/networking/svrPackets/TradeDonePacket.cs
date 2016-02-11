namespace wServer.networking.svrPackets
{
    public class TradeDonePacket : ServerPacket
    {
        public int Result { get; set; }
        public string Message { get; set; }

        public override PacketID ID { get { return PacketID.TradeDone; } }

        public override Packet CreateInstance()
        {
            return new TradeDonePacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Result = rdr.ReadInt32();
            Message = rdr.ReadUTF();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Result);
            wtr.WriteUTF(Message);
        }
    }
}
