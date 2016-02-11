namespace wServer.networking.cliPackets
{
    public class PlayerTextPacket : ClientPacket
    {
        public string Text { get; set; }

        public override PacketID ID { get { return PacketID.PlayerText; } }

        public override Packet CreateInstance()
        {
            return new PlayerTextPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Text = rdr.ReadUTF();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write32UTF(Text);
        }
    }
}
