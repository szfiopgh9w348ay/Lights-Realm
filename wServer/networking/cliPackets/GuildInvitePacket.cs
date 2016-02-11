namespace wServer.networking.cliPackets
{
    public class GuildInvitePacket : ClientPacket
    {
        public string Name;

        public override PacketID ID { get { return PacketID.GuildInvite; } }

        public override Packet CreateInstance()
        {
            return new GuildInvitePacket();
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
