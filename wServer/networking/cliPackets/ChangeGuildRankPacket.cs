namespace wServer.networking.cliPackets
{
    public class ChangeGuildRankPacket : ClientPacket
    {
        public string Name;
        public int GuildRank;

        public override PacketID ID { get { return PacketID.ChangeGuildRank; } }

        public override Packet CreateInstance()
        {
            return new ChangeGuildRankPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Name = rdr.ReadUTF();
            GuildRank = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(GuildRank);
        }
    }
}
