namespace wServer.networking.svrPackets
{
    public class InvitedToGuildPacket : ServerPacket
    {
        public string Name;
        public string Guild;

        public override PacketID ID { get { return PacketID.InvitedToGuild; } }

        public override Packet CreateInstance()
        {
            return new InvitedToGuildPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Name = rdr.ReadUTF();
            Guild = rdr.ReadUTF();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.WriteUTF(Guild);
        }
    }
}
