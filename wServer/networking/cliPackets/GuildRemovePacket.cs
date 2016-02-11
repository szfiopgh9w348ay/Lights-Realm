namespace wServer.networking.cliPackets
{
    public class GuildRemovePacket : ClientPacket
    {
        public string Name;

        public override PacketID ID { get { return PacketID.GuildRemove; } }

        public override Packet CreateInstance()
        {
            return new CreateGuildPacket();
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
