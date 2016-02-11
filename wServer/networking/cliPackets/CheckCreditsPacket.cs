namespace wServer.networking.cliPackets
{
    public class CheckCreditsPacket : ClientPacket
    {
        public override PacketID ID { get { return PacketID.CheckCredits; } }

        public override Packet CreateInstance()
        {
            return new CheckCreditsPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
        }

        protected override void Write(Client client, NWriter wtr)
        {
        }
    }
}
