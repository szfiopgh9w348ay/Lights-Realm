namespace wServer.networking.cliPackets
{
    public class RequestTradePacket : ClientPacket
    {
        public string Name { get; set; }

        public override PacketID ID { get { return PacketID.RequestTrade; } }

        public override Packet CreateInstance()
        {
            return new RequestTradePacket();
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
