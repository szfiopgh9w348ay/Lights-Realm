namespace wServer.networking.cliPackets
{
    public class AOEAckPacket : ClientPacket
    {
        public int Time { get; set; }
        public Position Position { get; set; }

        public override PacketID ID { get { return PacketID.AOEAck; } }

        public override Packet CreateInstance()
        {
            return new AOEAckPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Time = rdr.ReadInt32();
            Position = Position.Read(rdr);
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Time);
            Position.Write(wtr);
        }
    }
}
