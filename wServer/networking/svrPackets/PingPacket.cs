namespace wServer.networking.svrPackets
{
    public class PingPacket : ServerPacket
    {
        public int Serial { get; set; }

        public override PacketID ID { get { return PacketID.Ping; } }

        public override Packet CreateInstance()
        {
            return new PingPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Serial = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Serial);
        }
    }
}
